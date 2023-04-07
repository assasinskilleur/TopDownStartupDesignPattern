using UnityEngine;

[CreateAssetMenu(fileName = "Speed Modifier", menuName = "Stats Modifier/Speed Modifier")]
public class SpeedModifier : StatsModifier
{
    [SerializeField] private PlayerReference m_player;
    
    [SerializeField] private StatsModifierType m_modifierType;
    [SerializeField] float m_speedModifier;
    [SerializeField] int m_weight;
    
    public override void ApplyModifier()
    {
        m_player.Acquire().Stats.AddSpeedModifier(m_speedModifier, m_weight, m_modifierType);
    }
}
