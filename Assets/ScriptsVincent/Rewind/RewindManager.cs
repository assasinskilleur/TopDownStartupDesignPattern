using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class RewindManager : MonoBehaviour
{
    public static bool IsRewind { get; private set; }
    public event Action OnLaunchRewind;
    public event Action OnStopRewind;

    public event Action<float> OnUpdateRewind;

    [SerializeField]
    [ReadOnly]
    private List<GameAction> m_gameActions;
    [SerializeField] private float m_maxTimeRewind;
    [SerializeField] private RewindManagerReference m_reference;

    private float m_gameTime;
    private float m_maxGameTimeReached;

    private float MinGameTime
    {
        get
        {
            float l_minGameTime = m_maxGameTimeReached - m_maxTimeRewind;
            return (l_minGameTime > 0 ? l_minGameTime : 0);
        }
    }

    private float CurrentRewindState
    {
        get
        {
            float l_a = Mathf.InverseLerp(MinGameTime, m_maxGameTimeReached, m_gameTime);
            return l_a;
        }
    }

    private void Awake()
    {
        (m_reference as IReferenceHead<RewindManager>).Set(this);
    }

    private void Start()
    {
        m_gameActions = new List<GameAction>();
    }

    private void Update()
    {

        if (IsRewind)
        {
            m_gameTime += Time.deltaTime;
            if (m_gameTime > m_maxGameTimeReached)
            {
                m_maxGameTimeReached = m_gameTime;
                RemoveOldActions();
            }

        }
        else
        {
            m_gameTime -= Time.deltaTime;
            if (m_gameTime < MinGameTime)
            {
                IsRewind = false;
            }
            Rewind();

        }

        OnUpdateRewind?.Invoke(CurrentRewindState);
    }


    private void RemoveOldActions()
    {
        if (IsRewind)
        {
            return;
        }

        while (m_gameActions.Any() && m_gameActions[^1].GameTime < MinGameTime )
        {
            m_gameActions.RemoveAt(m_gameActions.Count - 1);
        }

    }

    public void RegisterInputs(InputActions p_inputs)
    {
        p_inputs.Player.Rewind.started += StartRewind;
        p_inputs.Player.Rewind.canceled += EndRewind;
    }



    public void AddAction(string p_name, Action p_action)
    {
        GameAction l_gameAction = new GameAction();
        l_gameAction.Name = p_name;
        l_gameAction.GameTime = m_gameTime;
        l_gameAction.A += p_action;

        m_gameActions.Insert(0, l_gameAction);
    }

    private void StartRewind(InputAction.CallbackContext p_context)
    {
        IsRewind = true;
        OnStartRewind();
    }

    private void OnStartRewind()
    {
        OnLaunchRewind?.Invoke();
    }

    private void Rewind()
    {
        PlayLastAction();
    }

    private void PlayLastAction()
    {
        if (!m_gameActions.Any()
            || m_gameActions[0].GameTime < m_gameTime)
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
        OnStopRewind?.Invoke();
    }
}

[Serializable]
public class GameAction
{
    [SerializeField]
    private string m_name;
    [SerializeField]
    private float m_gameTime;
    public string Name { get => m_name; set => m_name = value; }
    public float GameTime { get => m_gameTime; set => m_gameTime = value; }
    public event Action A;
    public event Action DeleteAction;

    public void Play()
    {
        A?.Invoke();
        A = null;
    }
}
