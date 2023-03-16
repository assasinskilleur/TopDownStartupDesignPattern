using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public event Action<int> UpdateInt;
    public event Action<string> UpdateString;

    public void InvokeUpdateScore(int newScore)
    {
        UpdateInt?.Invoke(newScore);
    }
}
