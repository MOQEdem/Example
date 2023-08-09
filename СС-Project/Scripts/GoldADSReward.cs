using UnityEngine;
using TMPro;
using System;
using Agava.YandexMetrica;

public class GoldADSReward : MonoBehaviour
{
    [SerializeField] private float _timeToCapture;
    [SerializeField] private Resource _resource;
    [SerializeField] private int _resourcesToSpawn;
    [SerializeField] private TMP_Text _countText;

    private Player _player;
    private Timer _timer;
    private PlayerTrigger _trigger;
    private ADSShower _adsShower;
    private bool _isPlayerRewarded = false;
    private float _delayBeforPlayADS = 1f;

    public event Action RewardUsed;

    private void Awake()
    {
        _adsShower = new ADSShower();
        _trigger = GetComponentInChildren<PlayerTrigger>(true);

        _countText.text = $"{_resourcesToSpawn + 5}";

        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
        _timer.Completed += OnTimerCompleted;

        _adsShower.Rewarded += OnRewarded;
        _adsShower.Closed += OnClosed;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
        _timer.Completed -= OnTimerCompleted;

        _adsShower.Rewarded -= OnRewarded;
        _adsShower.Closed -= OnClosed;
    }

    private void OnPlayerEnter(Player player)
    {
        _player = player;

        if (!_isPlayerRewarded)
            _timer.StartTimer(_delayBeforPlayADS);

    }

    private void OnPlayerExit(Player player)
    {
        if (_timer.IsWorking)
            _timer.StopTimer();
    }

    private void OnTimerCompleted()
    {
        TryWatchADS(_player.PlayerAccess.IsPlayerHaveAccess(AccessType.Gold));
    }

    public void TryWatchADS(bool isFreeADS)
    {

#if UNITY_EDITOR
        OnRewarded();
        return;
#endif
        if (isFreeADS)
            OnRewarded();
        else
            _adsShower.TryWatchADS();
    }

    private void OnRewarded()
    {
        if (!_isPlayerRewarded)
        {
#if !UNITY_EDITOR
            YandexMetrica.Send("goldADS");
#endif

            _isPlayerRewarded = true;

            RewardUsed?.Invoke();

            for (int i = 0; i < _resourcesToSpawn; i++)
            {
                Resource resource = Instantiate(_resource, this.transform);
                resource.transform.parent = null;

                Vector3 dropPoint = resource.transform.position + UnityEngine.Random.insideUnitSphere * 3;
                dropPoint.y = 0;

                resource.SetTarget(dropPoint);
            }

            Destroy(this.gameObject, 1f);
        }
    }

    private void OnClosed()
    {

    }
}
