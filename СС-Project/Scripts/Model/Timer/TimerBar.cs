using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private bool _isStartValueFull = false;

    private Timer _timer;

    private void Awake()
    {
        _timer = GetComponent<Timer>();

        _bar.fillAmount = _isStartValueFull ? 1f : 0f;
    }

    private void OnEnable()
    {
        SubscribeTimeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeTimeEvents();
    }

    private void OnTimerStart()
    {
        _bar.fillAmount = 0;
    }

    private void OnTimerUpdate()
    {
        _bar.fillAmount = _timer.Brain.Progres;
    }

    private void OnTimerStopped()
    {
        _bar.fillAmount = _isStartValueFull ? 1f : 0f;
    }

    private void OnTimerCompleted()
    {
        _bar.fillAmount = _isStartValueFull ? 1f : 0f;
    }

    private void SubscribeTimeEvents()
    {
        _timer.Started += OnTimerStart;
        _timer.Updated += OnTimerUpdate;
        _timer.Stopped += OnTimerStopped;
        _timer.Completed += OnTimerCompleted;
    }

    private void UnsubscribeTimeEvents()
    {
        _timer.Started -= OnTimerStart;
        _timer.Updated -= OnTimerUpdate;
        _timer.Stopped -= OnTimerStopped;
        _timer.Completed -= OnTimerCompleted;
    }
}
