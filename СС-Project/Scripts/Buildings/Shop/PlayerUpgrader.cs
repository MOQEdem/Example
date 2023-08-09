using System.Collections;
using UnityEngine;


public class PlayerUpgrader : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeBook _upgradeBook;
    [SerializeField] private StoreWarehouse _storeWarehouse;

    private StoreLot[] _lots;
    private StoreView _playerBalance;
    private StoreProgress _progress;
    private Player _player;
    private Coroutine _spendingResources;

    private void Awake()
    {
        _lots = GetComponentsInChildren<StoreLot>();
        _playerBalance = GetComponent<StoreView>();
        _progress = new StoreProgress();
        _progress.Load();
    }

    private void OnEnable()
    {
        foreach (StoreLot lot in _lots)
        {
            lot.ButtonDown += OnButtonDown;
            lot.ButtonUp += OnButtonUp;
            lot.AllDemandedResourceSpended += OnAllDemandedResourceSpended;
        }
    }

    private void OnDisable()
    {
        foreach (StoreLot lot in _lots)
        {
            lot.ButtonDown -= OnButtonDown;
            lot.ButtonUp -= OnButtonUp;
            lot.AllDemandedResourceSpended -= OnAllDemandedResourceSpended;
        }
    }

    private void Start()
    {
        foreach (StoreLot lot in _lots)
        {
            SetLotUpgrade(lot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_player == null)
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }

        UpdateView();
    }

    public void Save()
    {
        _progress.Save();
    }

    private void SetLotUpgrade(StoreLot lot)
    {
        int upgradeStep = _progress.GetLotStep(lot.Type);
        int spendedResources = _progress.GetLotSpendedResource(lot.Type);

        PlayerUpgrade upgrade = _upgradeBook.GetUpgrade(upgradeStep, lot.Type);
        lot.SetLotStatus(upgrade, spendedResources);
    }

    private void OnButtonDown(StoreLot lot)
    {
        StartSpending(lot);
    }

    private void OnButtonUp()
    {
        StopSpending();
    }

    private void OnAllDemandedResourceSpended(StoreLot lot)
    {
        StopSpending();

        _player.PlayerStatsManager.UpgradePlayerStat(lot.Upgrade.Value, lot.Type);

        _progress.UpLotStep(lot.Type);

        SetLotUpgrade(lot);

        UpdateView();
    }

    private void StartSpending(StoreLot lot)
    {
        if (_spendingResources == null)
            _spendingResources = StartCoroutine(SpendingResources(lot));
    }

    private void StopSpending()
    {
        if (_spendingResources != null)
        {
            StopCoroutine(_spendingResources);
            _spendingResources = null;
        }
    }

    private void UpdateView()
    {
        int balance = 0;

        if (_player != null)
            balance = _player.PlayerStack.EnemyResource.Count;

        foreach (var lot in _lots)
        {
            lot.UpdateInteractable(balance > 0);
            lot.UpdateStatInfo(_progress.GetLotStep(lot.Type));
        }

        _playerBalance.SetBalance(balance);
    }

    private IEnumerator SpendingResources(StoreLot lot)
    {
        var delay = new WaitForSeconds(0.3f);

        while (_player.PlayerStack.EnemyResource.Count > 0)
        {
            _storeWarehouse.ApplyResource(_player.PlayerStack.Transit(_player.PlayerStack.EnemyResource), true);
            lot.AddSpendResource();
            _progress.AddSpendedResource(lot.Type);
            UpdateView();

            yield return delay;
        }
    }
}
