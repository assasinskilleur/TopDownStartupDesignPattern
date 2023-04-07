using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField][Required] private PlayerReference m_playerReference;
    
    [SerializeField] private Movements m_movements;
    [SerializeField] private PlayerHealth m_playerHealth;
    [SerializeField] private PlayerStats m_playerStats;
    [SerializeField] private ShootOnClick m_shootOnClick;
    
    public Movements Movements => m_movements;
    public PlayerHealth Health => m_playerHealth;
    public PlayerStats Stats => m_playerStats;
    
    private float m_isShooting;

    private void Awake()
    {
        (m_playerReference as IReferenceHead<PlayerBrain>).Set(this);
        
        m_playerHealth.OnDamage += (damage, health) => Debug.Log($"Player took {damage} damage, current health: {health}");
    }

    private void Update()
    {
        if (m_isShooting > .1 && !RewindManager.IsRewind)
            m_shootOnClick.MakeAttack();
    }

    public void RegisterInputs(InputActions p_inputActions)
    {
        p_inputActions.Player.Direction.performed += GetMovementInputs;
        p_inputActions.Player.Shoot.started += GetShootInputs;
        p_inputActions.Player.Shoot.canceled += GetShootInputs;
    }

    private void GetMovementInputs(InputAction.CallbackContext p_context)
    {
        Vector2 m_directionInputs = p_context.ReadValue<Vector2>();
        if (RewindManager.IsRewind)
            return;

        Movements.Move(m_directionInputs * m_playerStats.Speed);
    }

    private void GetShootInputs(InputAction.CallbackContext obj)
    {
        if (RewindManager.IsRewind)
            return;

        m_isShooting = obj.ReadValue<float>();
    }
}
