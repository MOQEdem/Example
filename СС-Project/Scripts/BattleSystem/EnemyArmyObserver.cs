using System;
using UnityEngine;

public class EnemyArmyObserver : MonoBehaviour
{
    [SerializeField] private EnemyArmyController _enemyArmyController;

    private EnemyArmy _enemyArmy;
    private EnemySquad _currentTarget;

    public EnemyArmy CurrentEnemyArmy => _enemyArmy;
    public EnemySquad CurrentTarget => _currentTarget;

    public event Action TargetUpdated;
    public event Action<Transform> EnemyArmyDefeted;
    public event Action<LevelFinisher> LevelFinisherReached;

    private void OnEnable()
    {
        _enemyArmyController.NewArmySet += OnNewArmySet;
        _enemyArmyController.ArmyDefeted += OnArmyDefeted;
        _enemyArmyController.LevelFinisherReached += OnLevelFinisherReached;
    }

    private void OnDisable()
    {
        _enemyArmyController.NewArmySet -= OnNewArmySet;
        _enemyArmyController.ArmyDefeted -= OnArmyDefeted;
        _enemyArmyController.LevelFinisherReached -= OnLevelFinisherReached;
    }

    private void OnNewArmySet(EnemyArmy army)
    {
        _enemyArmy = army;
        _enemyArmy.SquadLost += OnEnemySquadEmpty;
        SetCurrentSquad();
    }

    private void OnArmyDefeted(EnemyArmy army)
    {
        _enemyArmy.SquadLost -= OnEnemySquadEmpty;
        EnemyArmyDefeted?.Invoke(army.LevelFinisher.transform);
    }

    private void OnEnemySquadEmpty()
    {
        SetCurrentSquad();
    }

    private void SetCurrentSquad()
    {
        _currentTarget = FindClosestSquad();

        if (_currentTarget != null)
            TargetUpdated?.Invoke();
    }

    private EnemySquad FindClosestSquad()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        EnemySquad closestSquad = null;

        if (_enemyArmy.Army.Count > 0)
        {
            foreach (var squad in _enemyArmy.Army)
            {
                Vector3 difference = squad.transform.position - position;
                float currentDistance = difference.sqrMagnitude;

                if (currentDistance < distance)
                {
                    closestSquad = squad;
                    distance = currentDistance;
                }
            }
        }

        return closestSquad;
    }

    private void OnLevelFinisherReached(LevelFinisher finisher)
    {
        LevelFinisherReached?.Invoke(finisher);
    }
}
