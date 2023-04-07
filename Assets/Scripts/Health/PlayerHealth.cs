using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private PlayerReference _player;
    [SerializeField] private OrganicDifficultyReference _diff;

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
        if (_player.Acquire().Stats.CurrentHealth <= 0) return;
        
        if (_player.Acquire().Stats.CurrentHealth >= _player.Acquire().Stats.MaxHealth) return;
        
        if (_regenTimer >= _regenTime)
        {
            Heal(_player.Acquire().Stats.Regen);
            _regenTimer = 0f;
        }
        
        _regenTimer += Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;
        
        _player.Acquire().Stats.CurrentHealth -= damage * _diff.Acquire()._diff;
        _diff.Acquire().LifeDown(damage * _diff.Acquire()._diff);
        OnDamage?.Invoke(damage, _player.Acquire().Stats.CurrentHealth);
        
        if (_player.Acquire().Stats.CurrentHealth <= 0)
        {
            _diff.Acquire().PlayerDeath();
            OnDeath?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        if (heal < 0) return;
        
        _player.Acquire().Stats.CurrentHealth += heal;

        if (_player.Acquire().Stats.CurrentHealth > _player.Acquire().Stats.MaxHealth)
        {
            _player.Acquire().Stats.CurrentHealth = _player.Acquire().Stats.MaxHealth;
        }
        
        OnHeal?.Invoke(heal, _player.Acquire().Stats.CurrentHealth);
    }
}
