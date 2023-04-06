using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Loot/New Loot")]
public class Loot : ScriptableObject
{
    [ShowAssetPreview(128, 128)]
    public Sprite m_lootSprite;
    
    public string m_lootName;
    public string m_lootDescription;
    public int m_dropChance;

    [HorizontalLine(color: EColor.Green)]
    [SerializeField] [Expandable] List<StatsModifier> m_statsModifiers;
    
    public List<StatsModifier> GetStatsModifiers()
    {
        return m_statsModifiers;
    }
}