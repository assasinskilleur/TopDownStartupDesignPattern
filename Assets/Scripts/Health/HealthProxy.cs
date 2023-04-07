using System;
using UnityEngine;

public class HealthProxy : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject m_healthGO;

    private IHealth m_health;
    
    private void Start()
    {
        m_health = m_healthGO.GetComponent<IHealth>();
    }

    public bool IsPlayer()
    {
        return m_health.IsPlayer();
    }

    public void TakeDamage(float damage)
    {
        m_health.TakeDamage(damage);
    }

    public void Heal(float heal)
    {
        m_health.Heal(heal);
    }
}
