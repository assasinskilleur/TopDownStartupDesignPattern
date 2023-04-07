using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [SerializeField] private PlayerReference m_playerReference;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_cooldown;
    
    private float m_currentCooldown;
    private bool m_isPlayerInRange;

    public void Update()
    {
        if (m_currentCooldown > 0)
            m_currentCooldown -= Time.deltaTime;
        
        if (CanAttack())
            MakeAttack();
    }
    
    bool CanAttack()
    {
        return m_currentCooldown <= 0 && m_isPlayerInRange;
    }
    
    public void MakeAttack()
    {
        m_currentCooldown = m_cooldown;
        
        m_playerReference.Acquire().Health.TakeDamage(m_damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHealth l_health))
        {
            if (l_health.IsPlayer())
            {
                m_isPlayerInRange = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHealth l_health))
        {
            if (l_health.IsPlayer())
            {
                m_isPlayerInRange = false;
            }
        }
    }
}
