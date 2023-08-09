using Agava.YandexMetrica;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMagnetGiver : MonoBehaviour
{
    [SerializeField] private float _timeToCapture;

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
        TryWatchADS(_player.PlayerAccess.IsPlayerHaveAccess(AccessType.Magnet));
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
            YandexMetrica.Send("magnetADS");
#endif

            _isPlayerRewarded = true;

            RewardUsed?.Invoke();

            _player.ResourceCollectorSwitcher.SwitchCollectors();

            Destroy(this.gameObject, 1f);
        }
    }

    private void OnClosed()
    {

    }
}
