using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Modifier", menuName = "Stats Modifier/Health Modifier")]
public class HealthModifier : StatsModifier
{
    [SerializeField] private PlayerReference m_player;
    
    [SerializeField] private StatsModifierType m_modifierType;
    [SerializeField] float m_healthModifier;
    [SerializeField] int m_weight;
    
    public override void ApplyModifier()
    {
        m_player.Acquire().Stats.AddMaxHealthModifier(m_healthModifier, m_weight, m_modifierType);
    }
}
