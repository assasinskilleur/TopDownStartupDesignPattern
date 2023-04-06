using UnityEngine;

[CreateAssetMenu(fileName = "Speed Modifier", menuName = "Stats Modifier/Speed Modifier")]
public class SpeedModifier : StatsModifier
{
    [SerializeField] private PlayerStatsReference m_playerStats;
    
    [SerializeField] private StatsModifierType m_modifierType;
    [SerializeField] float m_speedModifier;
    [SerializeField] int m_weight;
    
    public override void ApplyModifier()
    {
        m_playerStats.Acquire().AddSpeedModifier(m_speedModifier, m_weight, m_modifierType);
    }
}
