using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Movements m_movements;
    
    [SerializeField] private float m_speed;

    [SerializeField] private PlayerReference m_playerReference;

    private void Update()
    {
        if (!RewindManager.IsRewind)
            m_movements.Move((m_playerReference.Acquire().transform.position - transform.position).normalized * m_speed);
    }
}
