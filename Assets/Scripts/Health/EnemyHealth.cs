using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float m_maxHealth;
    
    public event Action OnDeath;
    public event Action<float, float> OnDamage;
    public event Action<float, float> OnHeal;
    
    private float m_currentHealth;
    
    private void Awake()
    {
        m_currentHealth = m_maxHealth;
        OnDamage += (damage, health) => Debug.Log($"Enemy took {damage} damage, current health: {health}");
    }

    public bool IsPlayer()
    {
        return false;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;
        
        m_currentHealth -= damage;
        
        OnDamage?.Invoke(damage, m_currentHealth);
        
        if (m_currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        if (heal < 0) return;
        
        m_currentHealth += heal;
        
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
        
        OnHeal?.Invoke(heal, m_currentHealth);
    }
}
