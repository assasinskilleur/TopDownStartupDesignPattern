using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private Sprite m_openedSprite;
    [SerializeField] private Sprite m_closedSprite;
    
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private BoxCollider2D m_boxCollider2D;
    [SerializeField] private LootBag m_lootBag;

    private void Awake()
    {
        m_spriteRenderer.sprite = m_closedSprite;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.TryGetComponent(out PlayerStats playerStats))
        {
            m_spriteRenderer.sprite = m_openedSprite;
            m_boxCollider2D.enabled = false;
            m_lootBag.InstantiateLoot(transform.position);
        }
    }
}
