using System;
using UnityEngine;

public class SquadPositionSetter : MonoBehaviour
{
    [SerializeField] private EnemySquad _selfSquad;
    [SerializeField] private SquadPositionSetter _trackedPositionSetter;

    public Vector3 CurrentTrackedPosition { get; private set; }
    public Vector3 NextTrackedPosition { get; private set; }
    public SquadPositionSetter TrackedPositionSetter => _trackedPositionSetter;

    public event Action SquadMoved;
    public event Action SquadDied;

    private void Awake() =>
        CurrentTrackedPosition = transform.position;

    private void Start()
    {
        if (_trackedPositionSetter)
            NextTrackedPosition = _trackedPositionSetter.CurrentTrackedPosition;
    }

    private void OnEnable()
    {
        _selfSquad.SquadEmpty += OnSelfSquadEmpty;
        if (_trackedPositionSetter)
        {
            _trackedPositionSetter.SquadMoved += OnTrackedSquadMoved;
            _trackedPositionSetter.SquadDied += OnTrackedSquadDied;
        }
    }

    private void OnDisable()
    {
        _selfSquad.SquadEmpty -= OnSelfSquadEmpty;
        if (_trackedPositionSetter)
        {
            _trackedPositionSetter.SquadMoved -= OnTrackedSquadMoved;
            _trackedPositionSetter.SquadDied -= OnTrackedSquadDied;
        }
    }

    private void OnSelfSquadEmpty(Squad squad) =>
        SquadDied?.Invoke();

    private void OnTrackedSquadDied()
    {
        _trackedPositionSetter.SquadMoved -= OnTrackedSquadMoved;
        _trackedPositionSetter.SquadDied -= OnTrackedSquadDied;

        SetNewPositions();

        CurrentTrackedPosition = NextTrackedPosition;
        if (_trackedPositionSetter)
            NextTrackedPosition = _trackedPositionSetter.NextTrackedPosition;
        SquadMoved?.Invoke();

        _trackedPositionSetter = _trackedPositionSetter.TrackedPositionSetter;
        if (_trackedPositionSetter)
        {
            _trackedPositionSetter.SquadMoved += OnTrackedSquadMoved;
            _trackedPositionSetter.SquadDied += OnTrackedSquadDied;
        }
    }

    private void OnTrackedSquadMoved()
    {
        SetNewPositions();

        CurrentTrackedPosition = NextTrackedPosition;
        NextTrackedPosition = _trackedPositionSetter.CurrentTrackedPosition;
        SquadMoved?.Invoke();
    }

    private void SetNewPositions()
    {
        foreach (NPC unit in _selfSquad.Units)
        {
            if (unit is NPCWalker walker)
            {
                Vector3 targetPosition = unit.transform.position + (NextTrackedPosition - CurrentTrackedPosition);
                walker.SetDefaultPosition(targetPosition);
                walker.GetComponent<NPCRandomPositionSetter>()?.SetDefaultPosition(targetPosition);
            }
        }
    }
}
