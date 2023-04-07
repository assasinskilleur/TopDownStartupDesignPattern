using UnityEngine;
using UnityEngine.UI;

public class HealthUpdater : MonoBehaviour
{
    [SerializeField] private PlayerReference m_playerReference;
    [SerializeField] private Image m_healthBar;

    private void Start()
    {
        m_playerReference.Acquire().Health.OnDamage += UpdateHealth;
        m_playerReference.Acquire().Health.OnHeal += UpdateHealth;
    }

    private void OnDestroy()
    {
        m_playerReference.Acquire().Health.OnDamage -= UpdateHealth;
        m_playerReference.Acquire().Health.OnHeal -= UpdateHealth;
    }

    private void UpdateHealth(float amount, float currentHealth)
    {
        m_healthBar.fillAmount = currentHealth / m_playerReference.Acquire().Stats.MaxHealth;
    }
}
