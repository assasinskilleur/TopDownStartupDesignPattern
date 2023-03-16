using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public class RewindManager : MonoBehaviour
{
    public event Action LaunchRewind;
    public event Action StopRewind;
    
    private float m_startTime;
    private float m_currentTimeRewinded;
    private List<GameAction> m_gameActions;
    private float m_totalTimeRewinded;

    private float GameTime => Time.time - m_totalTimeRewinded;

    [SerializeField] private float m_maxTimeRewind;

    public static bool IsRewind { get; private set; }
    private float NormalizedRewindValue => m_currentTimeRewinded / m_maxTimeRewind;

    [SerializeField] private UIRewind m_UI;

    private void Start()
    {
        m_gameActions = new List<GameAction>();
    }

    private void Update()
    {
        if (m_maxTimeRewind < GameTime)
        {
            RemoveOldActions();
        }
        
        if (IsRewind)
        {
            m_currentTimeRewinded += Time.deltaTime;
            m_totalTimeRewinded += Time.deltaTime;
            Rewind();
        }
        else
        {
            m_currentTimeRewinded -= Time.deltaTime;
        }

        m_currentTimeRewinded = Mathf.Clamp(m_currentTimeRewinded, 0, m_maxTimeRewind);
        m_UI?.UpdateFill(1 - NormalizedRewindValue);
    }

    private void RemoveOldActions()
    {
        if (IsRewind || !m_gameActions.Any())
            return;
        
        if (m_gameActions[^1].GameTime < GameTime - m_maxTimeRewind)
        {
            Debug.Log("Remove");
            m_gameActions.RemoveAt(m_gameActions.Count - 1);
            RemoveOldActions();
        }
    }

    public void RegisterInputs(InputActions p_inputs)
    {
        p_inputs.Player.Rewind.started += StartRewind;
        p_inputs.Player.Rewind.canceled += EndRewind;
    }



    public void AddAction(Action p_action, float p_time)
    {
        GameAction l_gameAction = new GameAction();
        l_gameAction.A += p_action;
        l_gameAction.GameTime = p_time - m_totalTimeRewinded;
        
        m_gameActions.Insert(0,l_gameAction);
    }

    private void StartRewind(InputAction.CallbackContext p_context)
    {
        IsRewind = true;
        m_startTime = GameTime;
        OnStartRewind();
    }

    private void OnStartRewind()
    {
        LaunchRewind?.Invoke();
    }

    private void Rewind()
    {
        PlayLastAction();
    }
    
    private void PlayLastAction()
    {
        Debug.Log(m_currentTimeRewinded);
        if (!m_gameActions.Any()
            || m_gameActions[0].GameTime < m_startTime - m_currentTimeRewinded)
            return;
        
        m_gameActions[0].Play();
        m_gameActions.RemoveAt(0);
        PlayLastAction();
        
    }

    private void EndRewind(InputAction.CallbackContext p_context)
    {
        IsRewind = false;

        OnEndRewind();
    }
    
    private void OnEndRewind()
    {
        StopRewind?.Invoke();
    }
}

public class GameAction
{
    public float GameTime { get; set; }
    public event Action A;
    public event Action DeleteAction;

    public void Play()
    {
        A?.Invoke();
        A = null;
    }
}
