using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TimerBrain _brain = new TimerBrain();
    private Coroutine _countingDown;

    public TimerBrain Brain => _brain;
    public bool IsWorking => _countingDown != null;

    public event Action Started;
    public event Action Stopped;
    public event Action Completed;
    public event Action Updated;

    private void OnEnable()
    {
        if (_brain != null)
            SubscribeTimeEvents();
    }

    private void OnDisable()
    {
        if (_brain != null)
            UnsubscribeTimeEvents();
    }

    public void StartTimer(float time)
    {
        if (_countingDown == null)
        {
            _countingDown = StartCoroutine(CountingDown(time));
        }
    }

    public void StopTimer()
    {
        if (_countingDown != null)
        {
            _brain.Stop();
            StopCoroutine(_countingDown);
            _countingDown = null;
        }
    }

    private IEnumerator CountingDown(float time)
    {
        _brain.Start(time);

        while (_brain.TimeLeft != 0)
        {
            yield return null;

            _brain.Tick(Time.deltaTime);
        }

        _countingDown = null;
    }

    private void OnTimerStart()
    {
        Started?.Invoke();
    }

    private void OnTimerUpdate()
    {
        Updated?.Invoke();
    }

    private void OnTimerStopped()
    {
        Stopped?.Invoke();
    }

    private void OnTimerCompleted()
    {
        Completed?.Invoke();
    }

    private void SubscribeTimeEvents()
    {
        _brain.Started += OnTimerStart;
        _brain.Updated += OnTimerUpdate;
        _brain.Stopped += OnTimerStopped;
        _brain.Completed += OnTimerCompleted;
    }

    private void UnsubscribeTimeEvents()
    {
        _brain.Started -= OnTimerStart;
        _brain.Updated -= OnTimerUpdate;
        _brain.Stopped -= OnTimerStopped;
        _brain.Completed -= OnTimerCompleted;
    }
}
