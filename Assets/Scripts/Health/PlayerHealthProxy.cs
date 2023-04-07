using UnityEngine;

public class PlayerHealthProxy : MonoBehaviour, IHealth
{
    [SerializeField] private PlayerHealth m_playerHealth;

    public void TakeDamage(float damage)
    {
        m_playerHealth.TakeDamage(damage);
    }

    public void Heal(float heal)
    {
        m_playerHealth.Heal(heal);
    }
}
