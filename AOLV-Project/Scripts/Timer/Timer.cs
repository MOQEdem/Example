using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private CanvasGroup _timerCanvasGroup;
    [SerializeField] private Image _timerImage;
    [SerializeField] private float _fadeDuration = 0.5f;

    private TimerBrain _timer = new TimerBrain();
    private Camera _camera;

    public TimerBrain TimerBrain => _timer;

    public event Action TimerStoped; 

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _timerCanvasGroup.alpha = 0f;

        if (_timer != null)
            SubscribeTimeEvents();
    }

    private void OnDisable()
    {
        if (_timer != null)
            UnsubscribeTimeEvents();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.up);
    }

    public IEnumerator CountingDown(float time)
    {
        _timer.Start(time);

        while (_timer.TimeLeft != 0)
        {
            _timer.Tick(Time.deltaTime);

            yield return null;
        }
    }

    private void OnTimerStart()
    {
        _timerCanvasGroup.DOKill();
        _timerCanvasGroup.DOFade(1, _fadeDuration);
    }

    private void OnTimerUpdate()
    {
        _timerImage.fillAmount = _timer.Progres;
    }

    private void OnTimerStopped()
    {
        _timerCanvasGroup.DOKill();
        _timerCanvasGroup.DOFade(0, _fadeDuration);

        _timerImage.fillAmount = 0f;
    }

    private void OnTimerCompleted()
    {
        _timerCanvasGroup.DOKill();
        _timerCanvasGroup.DOFade(0, _fadeDuration);

        _timerImage.fillAmount = 0f;
        TimerStoped?.Invoke();
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
