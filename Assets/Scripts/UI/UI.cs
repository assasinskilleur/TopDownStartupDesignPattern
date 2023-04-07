using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UI : MonoBehaviour
{
    public event Action<int> UpdateInt;
    public event Action<string> UpdateString;

    public event Action<InputsInjector.InputType> ChangeInput;

    [FormerlySerializedAs("m_menuScene")] [SerializeField] private string m_mainMenuSceneName;

    [SerializeField] private GameObject m_uiHandler;

    private bool m_MenuisOpen = false;

    public void InvokeUpdateScore(int newScore)
    {
        UpdateInt?.Invoke(newScore);
    }

    private void Awake()
    {
        m_MenuisOpen = m_uiHandler.activeSelf;
    }

    private void PressOpenCloseMenuInput(InputAction.CallbackContext p_context)
    {
        if (p_context.performed)
        {
            OpenCloseMenu();
        }
    }

   public void LoadMainMenu()
    {
        if (SceneManager.GetSceneByName(m_mainMenuSceneName) != null)
        {
            SceneManager.LoadScene(m_mainMenuSceneName);
        }
    }

    public void OpenCloseMenu()
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

    public void RegisterInputs(InputActions p_inputs)
    {
        p_inputs.Player.Pause.performed += PressOpenCloseMenuInput;
        p_inputs.UI.Resume.performed += PressOpenCloseMenuInput;
    }

}
