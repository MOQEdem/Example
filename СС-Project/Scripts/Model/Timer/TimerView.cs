using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    // [SerializeField] private CanvasGroup _timerCanvasGroup;
    [SerializeField] private TMP_Text _timerValue;
    [SerializeField] private TMP_Text _textWhenComplite;

    private Timer _timer;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        _timerValue.gameObject.SetActive(false);
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
        _timerValue.gameObject.SetActive(true);
        _textWhenComplite.gameObject.SetActive(false);
    }

    private void OnTimerUpdate()
    {
        _timerValue.text = $"{(int)_timer.Brain.TimeLeft / 60}:{(int)_timer.Brain.TimeLeft % 60}";
    }

    private void OnTimerStopped()
    {
        _timerValue.gameObject.SetActive(false);
        _textWhenComplite.gameObject.SetActive(true);
    }

    private void OnTimerCompleted()
    {
        _timerValue.gameObject.SetActive(false);
        _textWhenComplite.gameObject.SetActive(true);
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
