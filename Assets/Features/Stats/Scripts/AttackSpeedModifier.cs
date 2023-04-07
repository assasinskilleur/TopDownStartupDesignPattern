using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Speed Modifier", menuName = "Stats Modifier/Attack Speed Modifier")]
public class AttackSpeedModifier : StatsModifier
{
    [SerializeField] private PlayerReference m_player;
    
    [SerializeField] private StatsModifierType m_modifierType;
    [SerializeField] float m_attackSpeedModifier;
    [SerializeField] int m_weight;
    
    public override void ApplyModifier()
    {
        m_player.Acquire().Stats.AddAttackSpeedModifier(m_attackSpeedModifier, m_weight, m_modifierType);
    }
}
