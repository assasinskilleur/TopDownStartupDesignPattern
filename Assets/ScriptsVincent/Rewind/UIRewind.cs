using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewind : MonoBehaviour
{
    [SerializeField] private Image m_image;
    private float m_currentTime;

    public void UpdateFill(float p_value)
    {
        m_image.fillAmount = p_value;
    }
}
