using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private PlayerStatsReference _playerStats;

    public event Action OnDeath;
    public event Action<float, float> OnDamage;
    public event Action<float, float> OnHeal;

    // Heal every 1 second
    private float _regenTimer = 0f;
    private float _regenTime = 1f;
    
    private void Update()
    {
        AutoRegen();
    }
    
    private void AutoRegen()
    {
        if (_playerStats.Acquire().CurrentHealth <= 0) return;
        
        if (_playerStats.Acquire().CurrentHealth >= _playerStats.Acquire().MaxHealth) return;
        
        if (_regenTimer >= _regenTime)
        {
            Heal(_playerStats.Acquire().Regen);
            _regenTimer = 0f;
        }
        
        _regenTimer += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;
        
        _playerStats.Acquire().CurrentHealth -= damage;
        
        OnDamage?.Invoke(damage, _playerStats.Acquire().CurrentHealth);
        
        if (_playerStats.Acquire().CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        if (heal < 0) return;
        
        _playerStats.Acquire().CurrentHealth += heal;

        if (_playerStats.Acquire().CurrentHealth > _playerStats.Acquire().MaxHealth)
        {
            _playerStats.Acquire().CurrentHealth = _playerStats.Acquire().MaxHealth;
        }
        
        OnHeal?.Invoke(heal, _playerStats.Acquire().CurrentHealth);
    }
}
