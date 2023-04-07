using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Movements : MonoBehaviour, IRewindable
{
    [SerializeField] private RewindManagerReference m_rewind;

    private Vector2 m_oldVelocity;
    private Vector2 m_newVelocity;
    private Vector2 m_directionInputs;
    private Rigidbody2D m_rb;
    private Collider2D[] m_colliders;

    [SerializeField]
    private float m_delaySavePos;
    private float m_currentDelay;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_colliders = GetComponentsInChildren<Collider2D>();
        m_rewind.Acquire().OnLaunchRewind += OnStartRewind;
        m_rewind.Acquire().OnStopRewind += OnStopRewind;
        m_currentDelay = m_delaySavePos;
    }

    private void OnDestroy()
    {
        m_rewind.Acquire().OnLaunchRewind -= OnStartRewind;
        m_rewind.Acquire().OnStopRewind -= OnStopRewind;
    }

    private void FixedUpdate()
    {
        if (RewindManager.IsRewind)
        {
            m_rb.velocity = m_newVelocity;
        }
        else
        {
            m_rb.velocity = m_newVelocity * Time.fixedDeltaTime;
            
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
            if (m_oldVelocity != m_rb.velocity)
            {
                AddRewindAction(m_oldVelocity);
                m_oldVelocity = m_rb.velocity;
            }
        }
    }

    public void OnStartRewind()
    {
        Move(-m_rb.velocity);
        foreach (Collider2D l_col in m_colliders)
        {
            l_col.enabled = false;
        }
    }
    
    public void OnStopRewind()
    {
        foreach (Collider2D l_col in m_colliders)
        {
            l_col.enabled = true;
        }
        AddRewindAction(-m_newVelocity);
        Move(m_directionInputs);
        AddRewindAction(m_newVelocity);
    }

    public void Move(Vector2 p_velocity)
    {
        m_newVelocity = p_velocity;
    }

    public void AddRewindAction(object p_velocity)
    {
        Vector2 l_vector = (Vector2)p_velocity;
        m_rewind.Acquire().AddAction(String.Concat("Move ", p_velocity), () => Move(-l_vector));
    }

    private void RegisterPosition()
    {
        Vector3 l_pos = transform.position;
        m_rewind.Acquire().AddAction("Save Position", () => transform.position = l_pos);
    }
}
