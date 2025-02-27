using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Base Values
    [HorizontalLine(color: EColor.Blue)]
    [Header("Base Values")]
    [SerializeField] private float m_baseSpeed;
    [SerializeField] private float m_baseHealth;
    [SerializeField] private float m_baseRegen;
    [SerializeField] private float m_baseDamage;
    [SerializeField] private float m_baseAttackSpeed;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        m_speed = new Alterable<float>(m_baseSpeed);
        m_maxHealth = new Alterable<float>(m_baseHealth);
        m_regen = new Alterable<float>(m_baseRegen);
        m_damage = new Alterable<float>(m_baseDamage);
        m_attackSpeed = new Alterable<float>(m_baseAttackSpeed);
        
        m_health = m_maxHealth.CalculateValue();
    }
    
    #endregion

    #region Health

    private Alterable<float> m_maxHealth;
    
    private float m_health;
    
    private List<object> m_healthModifiers = new List<object>();
    public float CurrentHealth
    {
        get => m_health;
        set => m_health = value;
    }

    public float MaxHealth => m_maxHealth.CalculateValue();
    
    public void AddMaxHealthModifier(float p_value, int p_weight = 0, StatsModifierType p_modifierType = StatsModifierType.TotalMultiplicative, Func<float, float> p_modifierFunc = null)
    {
        object l_healthModifier;
        switch (p_modifierType)
        {
            case StatsModifierType.TotalMultiplicative:
                l_healthModifier = m_maxHealth.AddTransformator(hp => hp * p_value, p_weight);
                break;
            case StatsModifierType.Override:
                l_healthModifier = m_maxHealth.AddTransformator(hp => p_value, p_weight);
                break;
            case StatsModifierType.BaseAdditive:
                l_healthModifier = m_maxHealth.AddTransformator(hp => hp + m_baseHealth + p_value, p_weight);
                break;
            case StatsModifierType.BaseMultiplicative:
                l_healthModifier = m_maxHealth.AddTransformator(hp => hp * (m_baseHealth * p_value), p_weight);
                break;
            case StatsModifierType.Other:
                l_healthModifier = m_maxHealth.AddTransformator(p_modifierFunc, p_weight);
                break;
            default:
                l_healthModifier = m_maxHealth.AddTransformator(hp => hp + p_value, p_weight);
                break;
        }
        
        m_healthModifiers.Add(l_healthModifier);
    }

    #endregion

    #region Regen

    private Alterable<float> m_regen;
    
    private List<object> m_regenModifiers = new List<object>();
    
    public float Regen => m_regen.CalculateValue();
    
    public void AddRegenModifier(float p_value, int p_weight = 0, StatsModifierType p_modifierType = StatsModifierType.TotalMultiplicative, Func<float, float> p_modifierFunc = null)
    {
        object l_regenModifier;
        switch (p_modifierType)
        {
            case StatsModifierType.TotalMultiplicative:
                l_regenModifier = m_regen.AddTransformator(r => r * p_value, p_weight);
                break;
            case StatsModifierType.Override:
                l_regenModifier = m_regen.AddTransformator(r => p_value, p_weight);
                break;
            case StatsModifierType.BaseAdditive:
                l_regenModifier = m_regen.AddTransformator(r => r + m_baseRegen + p_value, p_weight);
                break;
            case StatsModifierType.BaseMultiplicative:
                l_regenModifier = m_regen.AddTransformator(r => r * (m_baseRegen * p_value), p_weight);
                break;
            case StatsModifierType.Other:
                l_regenModifier = m_regen.AddTransformator(p_modifierFunc, p_weight);
                break;
            default:
                l_regenModifier = m_regen.AddTransformator(r => r + p_value, p_weight);
                break;
        }
        
        m_regenModifiers.Add(l_regenModifier);
    }

    #endregion

    #region Damage

    private Alterable<float> m_damage;
    
    private List<object> m_damageModifiers = new List<object>();
    
    public float Damage => m_damage.CalculateValue();
    
    public void AddDamageModifier(float p_value, int p_weight = 0, StatsModifierType p_modifierType = StatsModifierType.TotalMultiplicative, Func<float, float> p_modifierFunc = null)
    {
        object l_damageModifier;
        switch (p_modifierType)
        {
            case StatsModifierType.TotalMultiplicative:
                l_damageModifier = m_damage.AddTransformator(d => d * p_value, p_weight);
                break;
            case StatsModifierType.Override:
                l_damageModifier = m_damage.AddTransformator(d => p_value, p_weight);
                break;
            case StatsModifierType.BaseAdditive:
                l_damageModifier = m_damage.AddTransformator(d => d + m_baseDamage + p_value, p_weight);
                break;
            case StatsModifierType.BaseMultiplicative:
                l_damageModifier = m_damage.AddTransformator(d => d * (m_baseDamage * p_value), p_weight);
                break;
            case StatsModifierType.Other:
                l_damageModifier = m_damage.AddTransformator(p_modifierFunc, p_weight);
                break;
            default:
                l_damageModifier = m_damage.AddTransformator(d => d + p_value, p_weight);
                break;
        }
        
        m_damageModifiers.Add(l_damageModifier);
    }
    
    #endregion
    
    #region Speed

    private Alterable<float> m_speed;
    
    private List<object> m_speedModifiers = new List<object>();
    public float Speed => m_speed.CalculateValue();
    
    public void AddSpeedModifier(float p_value, int p_weight = 0, StatsModifierType p_modifierType = StatsModifierType.TotalMultiplicative, Func<float, float> p_modifierFunc = null)
    {
        object l_speedModifier;
        switch (p_modifierType)
        {
            case StatsModifierType.TotalMultiplicative:
                l_speedModifier = m_speed.AddTransformator(sp => sp * p_value, p_weight);
                break;
            case StatsModifierType.Override:
                l_speedModifier = m_speed.AddTransformator(sp => p_value, p_weight);
                break;
            case StatsModifierType.BaseAdditive:
                l_speedModifier = m_speed.AddTransformator(sp => sp + m_baseSpeed + p_value, p_weight);
                break;
            case StatsModifierType.BaseMultiplicative:
                l_speedModifier = m_speed.AddTransformator(sp => sp * (m_baseSpeed * p_value), p_weight);
                break;
            case StatsModifierType.Other:
                l_speedModifier = m_speed.AddTransformator(p_modifierFunc, p_weight);
                break;
            default:
                l_speedModifier = m_speed.AddTransformator(sp => sp + p_value, p_weight);
                break;
        }
        
        m_speedModifiers.Add(l_speedModifier);
    }

    #endregion
    
    #region AttackSpeed
    
    private Alterable<float> m_attackSpeed;
    
    private List<object> m_attackSpeedModifiers = new List<object>();
    
    public float AttackSpeed => m_attackSpeed.CalculateValue();
    
    public void AddAttackSpeedModifier(float p_value, int p_weight = 0, StatsModifierType p_modifierType = StatsModifierType.TotalMultiplicative, Func<float, float> p_modifierFunc = null)
    {
        object l_attackSpeedModifier;
        switch (p_modifierType)
        {
            case StatsModifierType.TotalMultiplicative:
                l_attackSpeedModifier = m_attackSpeed.AddTransformator(asp => asp * p_value, p_weight);
                break;
            case StatsModifierType.Override:
                l_attackSpeedModifier = m_attackSpeed.AddTransformator(asp => p_value, p_weight);
                break;
            case StatsModifierType.BaseAdditive:
                l_attackSpeedModifier = m_attackSpeed.AddTransformator(asp => asp + m_baseAttackSpeed + p_value, p_weight);
                break;
            case StatsModifierType.BaseMultiplicative:
                l_attackSpeedModifier = m_attackSpeed.AddTransformator(asp => asp * (m_baseAttackSpeed * p_value), p_weight);
                break;
            case StatsModifierType.Other:
                l_attackSpeedModifier = m_attackSpeed.AddTransformator(p_modifierFunc, p_weight);
                break;
            default:
                l_attackSpeedModifier = m_attackSpeed.AddTransformator(asp => asp + p_value, p_weight);
                break;
        }
        
        m_attackSpeedModifiers.Add(l_attackSpeedModifier);
    }
    
    #endregion
}

public enum StatsModifierType
{
    TotalAdditive,
    TotalMultiplicative,
    Override,
    BaseAdditive,
    BaseMultiplicative,
    Other
}
