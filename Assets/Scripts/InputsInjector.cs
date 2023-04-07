using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsInjector : MonoBehaviour
{
    // Start is called before the first frame update

    public enum InputType
    {
        PLAYER,UI
    }

    private InputActions m_inputs;
    [SerializeField] private PlayerReference m_player;
    [SerializeField] private RewindManagerReference m_rewindManager;
    [SerializeField] private UI m_ui;
    
    void Start()
    {
        m_inputs = new InputActions();
        m_inputs.Enable();
        m_player.Acquire().RegisterInputs(m_inputs);
        m_rewindManager.Acquire().RegisterInputs(m_inputs);
        m_ui.ChangeInput += SwitchInput;
        m_ui.RegisterInputs(m_inputs);
    }

    private void OnDestroy()
    {
        m_ui.ChangeInput -= SwitchInput;
    }

    void SwitchInput(InputType p_inputType)
    {
        switch (p_inputType)
        {
            case InputType.PLAYER:
                m_inputs.Player.Enable();
                m_inputs.UI.Disable();
                break;
            case InputType.UI:
                m_inputs.Player.Disable();
                m_inputs.UI.Enable();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(p_inputType), p_inputType, null);
        }
    }
}
