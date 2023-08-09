using System;
using System.Collections;
using UnityEngine;

public class StableTimer : MonoBehaviour
{
    [SerializeField] private Bar _bar;
    [SerializeField] [Min(0)] private float _fillSpeed = 0.6f;

    private Coroutine _fillCoroutine;
    private float _currentFill;

    private void Start() => 
        _bar.SetFill(_currentFill);

    public void Start(Action onTimerFilledCallback)
    {
        if(_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);
        _fillCoroutine = StartCoroutine(FillCoroutine(1f, onTimerFilledCallback));
    }

    public void Stop()
    {
        if(_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);
        _fillCoroutine = StartCoroutine(FillCoroutine(0f));
    }

    private IEnumerator FillCoroutine(float targetFill, Action onTimerFilledCallback = null)
    {
        while (_currentFill != targetFill)
        {
            _currentFill = Mathf.MoveTowards(_currentFill, targetFill, _fillSpeed * Time.deltaTime);
            _bar.SetFill(_currentFill);
            yield return null;
        }
        _bar.SetFill(targetFill);
        
        if(targetFill == 1)
            onTimerFilledCallback?.Invoke();
    }
}
