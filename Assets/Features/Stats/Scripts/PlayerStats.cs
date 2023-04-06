using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsReference m_playerStatsReference;

    #region Base Values

    [Header("Base Values")]
    [SerializeField] private float m_baseSpeed;
    
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
            case StatsModifierType.TotalAdditive:
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
