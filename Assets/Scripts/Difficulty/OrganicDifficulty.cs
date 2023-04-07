using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicDifficulty : MonoBehaviour
{
    [SerializeField][Required] private OrganicDifficultyReference _organicDifficultyReference;

    private Difficulty _difficulty;

    private int _tokentUpDown = 0;
    private float _lifeDownCount = 0;
    private int _deathCount = 0;

    private float _minDiff;
    public float _diff { get; private set; }

    private void Awake()
    {
        (_organicDifficultyReference as IReferenceHead<OrganicDifficulty>).Set(this);
    }

    private void OnDestroy()
    {
        (_organicDifficultyReference as IReferenceHead<OrganicDifficulty>).Set(null);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) 
        {
            EnemyKill();
            Debug.Log(_diff);
        }
    }

    private void UpDownDiffyculty ()
    {
        if(_tokentUpDown >= 10)
        {
            _diff += 0.1f;
            _tokentUpDown = 0;

            if(_minDiff == _diff)
            {
                _minDiff += 0.1f;
                switch (_difficulty)
                {
                    case Difficulty.easy:
                        if(_minDiff > 0.5f)
                        {
                            _minDiff = 0.5f;
                        }
                        break;

                    case Difficulty.normal:
                        if (_minDiff > 1)
                        {
                            _minDiff = 1;
                        }
                        break;

                    case Difficulty.hard:
                        if (_minDiff > 2)
                        {
                            _minDiff = 2;
                        }
                        break;
                }
            }
        }
        else if(_tokentUpDown <= -10 && _minDiff < _diff)
        {
            _diff -= 0.1f;
            _tokentUpDown = 0;
        }

        if(_diff == 0.5f)
        {
            _difficulty = Difficulty.easy;
        }
        if (_diff == 1f)
        {
            _difficulty = Difficulty.normal;
        }
        if (_diff == 2f)
        {
            _difficulty = Difficulty.hard;
        }
    }

    public void EnemyKill()
    {
        _tokentUpDown++;
        UpDownDiffyculty();
    }

    public void PlayerDeath()
    {
        _tokentUpDown -= 2;
        _deathCount++;

        if (_deathCount > 2)
        {
            if (_minDiff>0.02f)
            {
                _minDiff -= 0.1f;
            }
            _deathCount = 0;
        }
        UpDownDiffyculty();
    }

    public void LifeDown(float takeDamage)
    {
        _lifeDownCount += takeDamage;
        
        if(_lifeDownCount >= 200)
        {
            _tokentUpDown--;
            _lifeDownCount -= 200;
        }
        UpDownDiffyculty();
    }

    public void SetDifficulty(Difficulty p_difficulty)
    {
        _difficulty = p_difficulty;
        switch (_difficulty)
        {
            case Difficulty.easy:
                _minDiff = 0.5f;
                _diff = _minDiff;
                break;

            case Difficulty.normal:
                _minDiff = 1;
                _diff = _minDiff;
                break;

            case Difficulty.hard:
                _minDiff = 2;
                _diff = _minDiff;
                break;
        }
    }
}

public enum Difficulty
{
    easy,
    normal,
    hard,
};
