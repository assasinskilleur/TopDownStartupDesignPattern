using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsInjector : MonoBehaviour
{
    // Start is called before the first frame update

    public enum InputType
    {
        Player,UI
    }

    private InputActions m_inputs;
    [SerializeField] private PlayerMovements m_playerMovements;
    [SerializeField] private RewindManager m_rewindManager;
    
    
    void Start()
    {
        m_inputs = new InputActions();
        m_inputs.Enable();
        m_playerMovements.RegisterInputs(m_inputs);
        m_rewindManager.RegisterInputs(m_inputs);
    }

    void SwitchInput(InputType p_inputType)
    {
        switch (p_inputType)
        {
            case InputType.Player:
                m_inputs.PLAYER.Enable();
                m_inputs.UI.Disable();
                break;
            case InputType.UI:
                m_inputs.PLAYER.Disable();
                m_inputs.UI.Enable();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(p_inputType), p_inputType, null);
        }
    }
}
