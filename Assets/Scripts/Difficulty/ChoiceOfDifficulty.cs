using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOfDifficulty : MonoBehaviour
{
    [SerializeField] private GameObject _canva;
    [SerializeField] private OrganicDifficultyReference m_refDiff;

    private void Start()
    {
        Time.timeScale = 0;
    }
    public void Easy()
    {
        m_refDiff.Acquire().SetDifficulty(Difficulty.easy) ;
        _canva.SetActive(false);
        Time.timeScale = 1;
    }

    public void Normal()
    {
        m_refDiff.Acquire().SetDifficulty(Difficulty.normal);
        _canva.SetActive(false);
        Time.timeScale = 1;
    }

    public void Hard()
    {
        m_refDiff.Acquire().SetDifficulty(Difficulty.hard);
        _canva.SetActive(false);
        Time.timeScale = 1;
    }
}
