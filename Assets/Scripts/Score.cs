using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    [SerializeField] private Text m_scoreField;
    [SerializeField] private UI m_ui;

    private int m_playerScore = 0;

    private int PlayerScore
    {
        get
        {
            if (m_playerScore <= 0)
                m_playerScore = 0;
            return  m_playerScore;
        }
    }

    private void Start()
    {
        m_ui.UpdateInt += UpdateScore;
    }

    private void OnDestroy()
    {
        m_ui.UpdateInt -= UpdateScore;
    }

    private void UpdateScore(int p_newScore)
    {
        string l_newScoreText = "PlayerScore : " + p_newScore;
        m_scoreField.text = l_newScoreText;
    }

    private void AddScore()
    {
        m_playerScore++;
        m_ui.InvokeUpdateScore(m_playerScore);
    }
}
