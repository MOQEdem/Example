using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyArmyController : MonoBehaviour
{
    private List<EnemyArmy> _armies;

    public EnemyArmy CurrentArmy => _armies[0];

    public event Action<EnemyArmy> NewArmySet;
    public event Action<EnemyArmy> ArmyDefeted;
    public event Action AllEnemiesDefeted;
    public event Action<LevelFinisher> LevelFinisherReached;

    private void Awake()
    {
        _armies = GetComponentsInChildren<EnemyArmy>(true).ToList();
    }

    private void OnEnable()
    {
        foreach (EnemyArmy army in _armies)
        {
            army.Defeated += OnArmyDefeated;
        }
    }

    private void OnDisable()
    {
        foreach (EnemyArmy army in _armies)
        {
            army.Defeated -= OnArmyDefeated;
        }
    }

    private void Start()
    {
        NewArmySet?.Invoke(_armies[0]);

        if (_armies.Count > 1)
        {
            for (int i = 1; i < _armies.Count; i++)
            {
                _armies[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnLevelFinisherReached(LevelFinisher finisher)
    {
        finisher.LevelComplite -= OnLevelFinisherReached;
        LevelFinisherReached?.Invoke(finisher);
    }

    private void OnArmyDefeated(EnemyArmy army)
    {
        army.LevelFinisher.LevelComplite += OnLevelFinisherReached;
        ArmyDefeted?.Invoke(army);
        _armies.Remove(army);
        army.Defeated -= OnArmyDefeated;

        if (_armies.Count == 0)
        {
            AllEnemiesDefeted?.Invoke();
        }
        else
        {
            _armies[0].gameObject.SetActive(true);

            NewArmySet?.Invoke(_armies[0]);
        }
    }
}
