using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewind : MonoBehaviour
{
    [SerializeField] private RewindManagerReference m_rewindManager;
    [SerializeField] private Image m_image;
    private float m_currentTime;

    private void Start()
    {
        m_rewindManager.Acquire().OnUpdateRewind += UpdateFill;
    }

    private void OnDestroy()
    {
        m_rewindManager.Acquire().OnUpdateRewind -= UpdateFill;
    }

    public void UpdateFill(float p_value)
    {
        m_image.fillAmount = p_value;
    }
}
