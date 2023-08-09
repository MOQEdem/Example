using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Cage : MonoBehaviour
{
    [SerializeField] private float _timeToCapture;
    [SerializeField] private float _animationTime;
    [SerializeField] private Resource _resource;
    [SerializeField] private int _resourcesToSpawn;

    private CapturedNPC _npc;
    private Timer _timer;
    private PlayerTrigger _trigger;
    private CageModel _model;
    private Coroutine _opening;

    public event Action<Cage> CageOpened;

    private void Awake()
    {
        _npc = GetComponentInChildren<CapturedNPC>();
        _timer = GetComponent<Timer>();
        _trigger = GetComponentInChildren<PlayerTrigger>(true);
        _model = GetComponentInChildren<CageModel>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
        _timer.Completed += OnTimerComplited;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
        _timer.Completed -= OnTimerComplited;
    }

    private void OnPlayerEnter(Player player)
    {
        _timer.StartTimer(_timeToCapture);
    }

    private void OnPlayerExit(Player player)
    {
        _timer.StopTimer();
    }

    private void OnTimerComplited()
    {
        if (_opening == null)
            _opening = StartCoroutine(Opening());
    }

    private IEnumerator Opening()
    {
        _trigger.gameObject.SetActive(false);

        transform.parent = null;

        Vector3 pointToMove = transform.position + Vector3.down * 1.5f;

        Tween cageMove = _model.transform.DOMove(pointToMove, _animationTime);
        yield return cageMove.WaitForCompletion();

        _npc.StartCelebrate();

        for (int i = 0; i < _resourcesToSpawn; i++)
        {
            Resource resource = Instantiate(_resource, _npc.transform);
            resource.transform.parent = null;

            Vector3 dropPoint = resource.transform.position + UnityEngine.Random.insideUnitSphere * 3;
            dropPoint.y = 0;

            resource.SetTarget(dropPoint);
        }

        CageOpened?.Invoke(this);

        // Destroy(this.gameObject, 1f);
    }
}
