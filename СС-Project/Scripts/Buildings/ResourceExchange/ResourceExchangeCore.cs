using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ResourceExchangeCore : MonoBehaviour
{
    [SerializeField] private ResourceExchangeEnter _resourceSource;
    [SerializeField] private float _timeToExchange;
    [SerializeField] private Transform _exchangePoint;

    private List<Resource> _resources = new List<Resource>();
    private Resource _resource;
    private Timer _timer;
    private Coroutine _exchanging;
    private bool _isCurrentResourceExchanged = false;

    public bool IsHaveExchangedResource => _resources.Count > 0;
    public List<Resource> Resources => _resources;

    public event Action<Resource> CountChanged;

    private void Awake()
    {
        _resource = GetComponentInChildren<Resource>();
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _resourceSource.ChangeCount += OnChangeCount;
        _timer.Completed += OnTimerComplited;
    }

    private void OnDisable()
    {
        _resourceSource.ChangeCount -= OnChangeCount;
        _timer.Completed -= OnTimerComplited;
    }

    public Resource SpendResource()
    {
        if (_resources.Count > 0)
        {
            Resource resource = _resources[_resources.Count - 1];
            _resources.RemoveAt(_resources.Count - 1);
            _resource.transform.DOKill();
            return resource;
        }

        return null;
    }

    private void OnChangeCount()
    {
        if (_exchanging == null && _resourceSource.Resources.Count > 0)
        {
            _exchanging = StartCoroutine(Exchanging());
            _resourceSource.ChangeCount -= OnChangeCount;
        }
    }

    private void OnTimerComplited()
    {
        _isCurrentResourceExchanged = true;
    }

    private IEnumerator Exchanging()
    {
        float animationTime = 1f;

        while (_resourceSource.Resources.Count > 0)
        {
            var resource = _resourceSource.Resources[_resourceSource.Resources.Count - 1];

            resource.transform.DOKill();

            _resourceSource.SpendResource();

            Tween movement = resource.transform.DOMove(_exchangePoint.transform.position, animationTime);
            yield return movement.WaitForCompletion();

            _timer.StartTimer(_timeToExchange);

            Destroy(resource.gameObject);

            while (!_isCurrentResourceExchanged)
            {
                yield return null;
            }

            _timer.StopTimer();

            resource = Instantiate(_resource, _exchangePoint.transform);
            resource.transform.localPosition = Vector3.zero;
            resource.transform.parent = null;
            resource.PickUp();

            _resources.Add(resource);
            CountChanged?.Invoke(resource);

            _isCurrentResourceExchanged = false;
        }

        _resourceSource.ChangeCount += OnChangeCount;
        _exchanging = null;
    }
}
