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
    private Vector2 m_directionInputs;
    private Rigidbody2D m_rb;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rewind.LaunchRewind += InvertVelocity;
        m_rewind.StopRewind += InputsMove;
    }

    private void OnDestroy()
    {
        m_rewind.LaunchRewind -= InvertVelocity;
        m_rewind.StopRewind -= InputsMove;
    }

    public void RegisterInputs(InputActions p_inputActions)
    {
        p_inputActions.Player.Direction.performed += GetMovementInputs;
    }

    private void GetMovementInputs(InputAction.CallbackContext p_context)
    {
        m_directionInputs = p_context.ReadValue<Vector2>();
        if (RewindManager.IsRewind)
            return;

        InputsMove();
    }

    private void InputsMove()
    {
        Move(m_directionInputs);
    }

    private void Move(Vector2 p_direction)
    {
        Vector2 l_vel = p_direction * m_speed;
        Vector2 l_oldVel = m_rb.velocity;
        m_rb.velocity = l_vel;
        
        m_rewind.AddAction(() => UndoMove(-l_oldVel), Time.time);

    }

    private void UndoMove(Vector3 p_velocity)
    {
        m_rb.velocity = p_velocity;
    }

    private void InvertVelocity()
    {
        m_rb.velocity = -m_rb.velocity;
    }
}
