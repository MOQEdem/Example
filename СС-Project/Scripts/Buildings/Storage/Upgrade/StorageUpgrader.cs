using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUpgrader : MonoBehaviour
{
    [SerializeField] private UpgradeZone _playerData;
    [SerializeField] private UpgraderStagesBook _stages;

    private const string SaveKey = nameof(StorageUpgrader);
    private StorageEnter _storage;
    private StorageUpgraderStage[] _allStages;
    private UpgraderStatus _status;
    private List<UpgraderStageData> _availableUpgrades;
    private PlayerTrigger _trigger;
    private Coroutine _waitingToPlayerStop;
    private StorageADS _storageADS;
    private Collider _collider;
    private StorageUpgraderView _storageUpgraderView;

    private void Awake()
    {
        _status = new UpgraderStatus(SaveKey);
        _status.Load();

        _storageADS = GetComponent<StorageADS>();
        _trigger = GetComponent<PlayerTrigger>();
        _collider = GetComponent<Collider>();
        _storage = GetComponentInChildren<StorageEnter>();
        _storageUpgraderView = GetComponentInChildren<StorageUpgraderView>();

        _availableUpgrades = _stages.GetListOfAvailableUpgrades();
        _allStages = GetComponentsInChildren<StorageUpgraderStage>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
    }

    private void Start()
    {
        TryFindNextUpgrade();

        ActivateCurrenStorage();
    }

    public void Save()
    {
        _status.Save();
    }

    private void OnPlayerEnter(Player player)
    {
        if (_waitingToPlayerStop == null)
        {
            _waitingToPlayerStop = StartCoroutine(WaitingToPlayerStop(player));
        }
    }

    private void OnPlayerExit(Player player)
    {
        if (_waitingToPlayerStop != null)
        {
            StopCoroutine(_waitingToPlayerStop);
            _waitingToPlayerStop = null;
        }
    }

    private IEnumerator WaitingToPlayerStop(Player player)
    {
        bool isWorking = true;

        while (isWorking)
        {
            if (player.PlayerStack.IsReadyToTransit)
            {
                if (!_storageADS.IsWatched)
                {
                    _storageADS.TryWatchADS();
                }
                else
                {
                    _status.LevelUp();

                    TryFindNextUpgrade();

                    ActivateCurrenStorage();

                    _storageADS.ResetWatchedStatus();
                    isWorking = false;
                }
            }

            yield return null;
        }

        _waitingToPlayerStop = null;
    }

    private void TryFindNextUpgrade()
    {
        if (_availableUpgrades[_availableUpgrades.Count - 1].Level > _status.GetCurrentUpgradeLevel())
        {
            foreach (var upgrade in _availableUpgrades)
            {
                if (upgrade.Level == _status.GetCurrentUpgradeLevel() + 1)
                {
                    if (upgrade.MinPlayerLevel <= _playerData.SquadLevel)
                    {
                        ChangeState(true);
                    }
                    else
                    {
                        ChangeState(false);
                    }
                }
            }
        }
        else
        {
            ChangeState(false);
        }
    }

    private void ActivateCurrenStorage()
    {
        foreach (var stage in _allStages)
        {
            if (stage.Level == _status.GetCurrentUpgradeLevel())
            {
                stage.SetMeshStatus(true);
                _storage.SetNewCapacity(stage.Capacity);
            }
            else
            {
                stage.SetMeshStatus(false);
            }
        }
    }

    private void ChangeState(bool isActive)
    {
        _collider.enabled = isActive;
        _storageUpgraderView.gameObject.SetActive(isActive);
    }
}
