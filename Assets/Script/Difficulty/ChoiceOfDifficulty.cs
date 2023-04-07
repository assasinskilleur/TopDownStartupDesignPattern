using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOfDifficulty : MonoBehaviour
{
    [SerializeField] private GameObject _canva;

    private void Start()
    {
        Time.timeScale = 0;
    }
    public void Easy()
    {
        Debug.Log("easy");
        //organicDifficulty._difficulty = Difficulty.easy:
        _canva.SetActive(false);
        Time.timeScale = 1;
    }

    public void Normal()
    {
        Debug.Log("normal");

        //organicDifficulty._difficulty = Difficulty.normal:
        _canva.SetActive(false);
        Time.timeScale = 1;
    }

    public void Hard()
    {
        Debug.Log("hard");

        //organicDifficulty._difficulty = Difficulty.hard:
        _canva.SetActive(false);
        Time.timeScale = 1;
    }
}
