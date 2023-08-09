using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeZone : InteractiveZone
{
    [SerializeField] private PlayerSquadUpgradeRequirements _requirements;

    private PlayerSquadLevel _level;
    private int _gameLevel;

    public int SquadLevel => _level.Level;

    public event Action<Dictionary<PlayerSquadZoneType, int>> LevelSet;
    public event Action MaxLevelSet;

    private void Awake()
    {
        _level = new PlayerSquadLevel();
        _level.Load();
        _gameLevel = SceneManager.GetActiveScene().buildIndex;
        SetCost();
    }

    protected override void Start()
    {
        base.Start();
        LevelSet?.Invoke(_requirements.GetCapacity(_level.Level));
    }

    public void Save()
    {
        _level.Save();
    }

    protected override void Activate()
    {
        _level.LevelUp();
        ResetResourceStack();

        SetCost();

        LevelSet?.Invoke(_requirements.GetCapacity(_level.Level));
    }

    private void SetCost()
    {
        if (_requirements.IsAbleToUpgrade(_gameLevel, _level.Level, out int cost))
        {
            _startCount = cost;
            SetTargetCountResource(cost);
        }
        else
        {
            MaxLevelSet?.Invoke();
            ChangeState(true);
            _startCount = 0;
        }

        ChangeCount?.Invoke();
    }
}
