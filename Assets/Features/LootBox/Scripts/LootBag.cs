using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField][Required] private GameObject droppedItemPrefab;
    [SerializeField][Expandable] private List<Loot> m_loots;

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleLoots = new List<Loot>();
        foreach (var loot in m_loots)
        {
            if (randomNumber <= loot.m_dropChance)
            {
                possibleLoots.Add(loot);
            }
        }
        
        if (possibleLoots.Count > 0)
        {
            return possibleLoots[Random.Range(0, possibleLoots.Count)];
        }
        else
        {
            return m_loots[Random.Range(0, m_loots.Count)];
        }
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Loot droppedItem = GetDroppedItem();
        GameObject droppedItemObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        droppedItemObject.GetComponent<DroppedLoot>().SetLoot(droppedItem);
    }
}
