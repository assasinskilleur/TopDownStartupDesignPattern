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
    [SerializeField] private RewindManager m_rewind;

    [SerializeField] private float m_speed;
    private Vector2 m_playerVelocity;
    private Vector2 m_directionInputs;
    private Rigidbody2D m_rb;

    [SerializeField]
    private float m_delaySavePos;
    private float m_currentDelay;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rewind.LaunchRewind += InvertVelocity;
        m_rewind.StopRewind += OnStopRewind;
        m_currentDelay = m_delaySavePos;
    }

    private void OnDestroy()
    {
        m_rewind.LaunchRewind -= InvertVelocity;
        m_rewind.StopRewind -= OnStopRewind;
    }

    private void FixedUpdate()
    {
        m_rb.velocity = m_playerVelocity * Time.fixedDeltaTime;

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

        Move(m_directionInputs);
        AddRewindMove(l_oldDir);
    }
    
    private void OnStopRewind()
    {
        AddRewindMove(-m_playerVelocity.normalized);
        Move(m_directionInputs);
        AddRewindMove(m_directionInputs);
    }

    private void Move(Vector2 p_direction)
    {
        m_playerVelocity = p_direction * m_speed;
    }

    private void AddRewindMove(Vector3 p_direction)
    {
        m_rewind.AddAction(String.Concat("Move ", p_direction), () => Move(-p_direction));
    }

    private void RegisterPosition()
    {
        Vector3 l_pos = transform.position;
        m_rewind.AddAction("Save Position", () => transform.position = l_pos);
    }

    private void InvertVelocity()
    {
        m_playerVelocity = -m_playerVelocity;
    }
}
