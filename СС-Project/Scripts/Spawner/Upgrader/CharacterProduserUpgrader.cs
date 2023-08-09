using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterProduserUpgrader : InteractiveZone
{
    [SerializeField] private UpgradeZone _playerData;
    [SerializeField] private UpgraderStagesBook _stages;

    private CharacterProduser[] _produsers;
    private UpgraderStatus _currentStatus;
    private List<UpgraderStageData> _availableUpgrades;

    protected abstract UpgraderStatus InitUpgraderStatus();

    private void Awake()
    {
        _currentStatus = InitUpgraderStatus();
        _currentStatus.Load();
        _availableUpgrades = _stages.GetListOfAvailableUpgrades();

        _produsers = GetComponentsInChildren<CharacterProduser>(true);
    }

    protected override void Start()
    {
        SetCost();

        ActivateCurrenProduser();
    }

    private void OnEnable()
    {
        _playerData.LevelSet += OnLevelSet;
    }

    private void OnDisable()
    {
        _playerData.LevelSet -= OnLevelSet;
    }

    private void OnLevelSet(Dictionary<PlayerSquadZoneType, int> dictionary)
    {
        SetCost();
    }

    protected override void Activate()
    {
        _currentStatus.LevelUp();
        ResetResourceStack();

        SetCost();

        ActivateCurrenProduser();
    }

    public void Save()
    {
        _currentStatus.Save();
    }

    private void SetCost()
    {
        if (_availableUpgrades[_availableUpgrades.Count - 1].Level > _currentStatus.GetCurrentUpgradeLevel())
        {
            foreach (var upgrade in _availableUpgrades)
            {
                if (upgrade.Level == _currentStatus.GetCurrentUpgradeLevel() + 1)
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

    private void ActivateCurrenProduser()
    {
        foreach (var produser in _produsers)
            if (produser.Level == _currentStatus.GetCurrentUpgradeLevel())
                produser.gameObject.SetActive(true);
            else
                produser.gameObject.SetActive(false);
    }
}
