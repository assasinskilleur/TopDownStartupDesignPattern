using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Modifier", menuName = "Stats Modifier/Damage Modifier")]
public class DamageModifier : StatsModifier
{
    [SerializeField] private PlayerReference m_player;
    
    [SerializeField] private StatsModifierType m_modifierType;
    [SerializeField] float m_damageModifier;
    [SerializeField] int m_weight;
    
    public override void ApplyModifier()
    {
        m_player.Acquire().Stats.AddDamageModifier(m_damageModifier, m_weight, m_modifierType);
    }
}
