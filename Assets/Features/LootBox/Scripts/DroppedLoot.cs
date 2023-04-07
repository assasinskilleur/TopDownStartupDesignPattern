using UnityEngine;

public class DroppedLoot : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] Rigidbody2D m_rigidbody2D;
    [SerializeField] private float dropForce = 250f;
    
    private Loot m_loot;
    
    public void SetLoot(Loot droppedItem)
    {
        m_loot = droppedItem;
        m_spriteRenderer.sprite = droppedItem.m_lootSprite;
        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        m_rigidbody2D.AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealthProxy playerStats))
        {
            foreach (var statsModifier in m_loot.GetStatsModifiers())
            {
                statsModifier.ApplyModifier();
            }
            Destroy(gameObject);
        }
    }
}
