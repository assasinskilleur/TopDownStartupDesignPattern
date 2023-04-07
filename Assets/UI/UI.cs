using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI : MonoBehaviour
{
    public event Action<int> UpdateInt;
    public event Action<string> UpdateString;

    public event Action<InputsInjector.InputType> ChangeInput;

    [SerializeField] private GameObject m_uiHandler;

    private bool m_MenuisOpen = false;

    public void InvokeUpdateScore(int newScore)
    {
        UpdateInt?.Invoke(newScore);
    }

    private void OpenCloseMenu(InputAction.CallbackContext p_context)
    {
        if (p_context.performed)
        {
            if (m_MenuisOpen)
            {
                ChangeInput?.Invoke(InputsInjector.InputType.PLAYER);
            }
            else
            {
                ChangeInput?.Invoke(InputsInjector.InputType.UI);
            }
            m_MenuisOpen = !m_MenuisOpen;
            m_uiHandler.SetActive(m_MenuisOpen);

        }
    }

    public void RegisterInputs(InputActions p_inputs)
    {
        p_inputs.Player.Pause.performed += OpenCloseMenu;
        p_inputs.UI.Resume.performed += OpenCloseMenu;
    }

}
