using NaughtyAttributes;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField][Required] private PlayerReference m_playerReference;
    
    [SerializeField] private PlayerMovements m_playerMovements;
    [SerializeField] private PlayerHealth m_playerHealth;
    [SerializeField] private PlayerStats m_playerStats;
    
    public PlayerMovements Movements => m_playerMovements;
    public PlayerHealth Health => m_playerHealth;
    public PlayerStats Stats => m_playerStats;

    private void Awake()
    {
        (m_playerReference as IReferenceHead<Player>).Set(this);
    }
}
