using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private PlayerStatsReference m_playerStats;
    [SerializeField] private RewindManagerReference m_rewind;

    [SerializeField] private float m_speed;
    private Vector2 m_oldVelocity;
    private Vector2 m_newPlayerVelocity;
    private Vector2 m_directionInputs;
    private Rigidbody2D m_rb;

    [SerializeField]
    private float m_delaySavePos;
    private float m_currentDelay;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rewind.Acquire().OnLaunchRewind += InvertVelocity;
        m_rewind.Acquire().OnStopRewind += OnStopRewind;
        m_currentDelay = m_delaySavePos;
    }

    private void OnDestroy()
    {
        m_rewind.Acquire().OnLaunchRewind -= InvertVelocity;
        m_rewind.Acquire().OnStopRewind -= OnStopRewind;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (RewindManager.IsRewind)
        {
            m_rb.velocity = m_newPlayerVelocity;
        }
        else
        {
            m_rb.velocity = m_newPlayerVelocity * Time.fixedDeltaTime;
            if (!RewindManager.IsRewind)
            {
                m_currentDelay -= Time.fixedDeltaTime;
                if (m_currentDelay <= 0)
                {
                    m_currentDelay = m_delaySavePos;
                    RegisterPosition();
                }

                
                
            }
        }
    }

    private void LateUpdate()
    {
        if (!RewindManager.IsRewind)
        {
            Debug.Log(m_oldVelocity + "   " + m_rb.velocity + "   " + (m_oldVelocity != m_rb.velocity));
            if (m_oldVelocity != m_rb.velocity)
            {
                Debug.Log("AddRewind");
                AddRewindMove(m_oldVelocity);
                m_oldVelocity = m_rb.velocity;
            }
        }
    }

    public void RegisterInputs(InputActions p_inputActions)
    {
        p_inputActions.Player.Direction.performed += GetMovementInputs;
    }

    private void GetMovementInputs(InputAction.CallbackContext p_context)
    {
        Debug.Log("Inputs");
        Vector2 l_oldDir = m_directionInputs;
        m_directionInputs = p_context.ReadValue<Vector2>();
        if (RewindManager.IsRewind)
            return;

        Move(m_directionInputs * m_speed);
    }
    
    private void OnStopRewind()
    {
        AddRewindMove(-m_newPlayerVelocity.normalized);
        Move(m_directionInputs);
        AddRewindMove(m_directionInputs);
    }

    private void Move(Vector2 p_velocity)
    {
        m_newPlayerVelocity = p_velocity;
    }

    private void AddRewindMove(Vector3 p_velocity)
    {
        m_rewind.AddAction(String.Concat("Move ", p_velocity), () => Move(-p_velocity));
    }

    private void RegisterPosition()
    {
        // Vector3 l_pos = transform.position;
        // m_rewind.AddAction("Save Position", () => transform.position = l_pos);
    }

    private void InvertVelocity()
    {
        m_newPlayerVelocity = -m_newPlayerVelocity;
    }
}
