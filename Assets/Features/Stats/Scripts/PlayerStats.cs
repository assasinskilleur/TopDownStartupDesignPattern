using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsReference m_playerStatsReference;

    #region Base Values
    [HorizontalLine(color: EColor.Blue)]
    [Header("Base Values")]
    [SerializeField] private float m_baseSpeed;
    [SerializeField] private float m_baseHealth;
    [SerializeField] private float m_baseRegen;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        (m_playerStatsReference as IReferenceHead<PlayerStats>).Set(this);
        m_speed = new Alterable<float>(m_baseSpeed);
    }
    
    private void OnDestroy()
    {
        (m_playerStatsReference as IReferenceHead<PlayerStats>).Set(null);
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
