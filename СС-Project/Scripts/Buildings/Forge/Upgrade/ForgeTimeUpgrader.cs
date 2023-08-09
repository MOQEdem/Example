using System.Collections.Generic;
using UnityEngine;

public abstract class ForgeTimeUpgrader : InteractiveZone
{
    [SerializeField] private UpgradeZone _playerData;
    [SerializeField] private UpgraderStagesBook _stages;
    [SerializeField] private ForgeUpgraderStages _allStages;
    [SerializeField] private ForgeResourceProducer _resourceProducer;
    [SerializeField] private BuffZone _zone;

    protected abstract UpgraderStatus InitUpgraderStatus();

    private UpgraderStatus _status;
    private List<UpgraderStageData> _availableUpgrades;

    private void Awake()
    {
        _status = InitUpgraderStatus();
        _status.Load();

        _availableUpgrades = _stages.GetListOfAvailableUpgrades();
    }

    protected override void Start()
    {
        SetCost();

        ApplyNewTimeAndCapacitye();
    }

    protected override void Activate()
    {
        _status.LevelUp();
        ResetResourceStack();

        SetCost();

        ApplyNewTimeAndCapacitye();
    }

    public void Save()
    {
        _status.Save();
    }

    private void SetCost()
    {
        if (_availableUpgrades[_availableUpgrades.Count - 1].Level > _status.GetCurrentUpgradeLevel())
        {
            foreach (var upgrade in _availableUpgrades)
            {
                if (upgrade.Level == _status.GetCurrentUpgradeLevel() + 1)
                {
                    if (upgrade.MinPlayerLevel <= _playerData.SquadLevel)
                    {
                        Debug.Log(upgrade.MinPlayerLevel);
                        ChangeState(false);
                        SetTargetCountResource(upgrade.Cost);
                    }
                    else
                    {
                        ChangeState(true);
                        _startCount = 0;
                    }
                }
            }
        }
        else
        {
            ChangeState(true);
            _startCount = 0;
        }

        ChangeCount?.Invoke();
    }

    private void ApplyNewTimeAndCapacitye()
    {
        foreach (var stage in _allStages.Stages)
        {
            if (stage.Level == _status.GetCurrentUpgradeLevel())
            {
                _resourceProducer.SetProducingStepTime(stage.Time);
                _zone.SetNewCapacity(stage.Capacity);
            }
        }
    }
}
