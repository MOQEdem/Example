using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSquadZone : MonoBehaviour
{
    [SerializeField] private PlayerSquadZoneType _type;
    [SerializeField] private Transform[] _totalPoints;
    [SerializeField] private PlayerSquadZoneSizeController _sizeController;
    [SerializeField] private PlayerSquad _playerSquad;

    private BaseMover _baseMover;
    private int _numberOfCurrentPoint;
    private int _currentCapacity;

    public bool IsFull { get; private set; }

    public Transform CurrentPoint { get; private set; }
    public int NumberOfCurrentPoint => _numberOfCurrentPoint;

    public Action ReadyForBattle;

    private void Awake()
    {
        _baseMover = GetComponentInParent<BaseMover>();
    }

    private void OnEnable()
    {
        _baseMover.BaseMoved += OnBaseMoved;
        _playerSquad.BattleStarted += OnBattleStarted;
        _playerSquad.NewCapacitySet += OnNewCapacitySet;
    }

    private void OnDisable()
    {
        _baseMover.BaseMoved -= OnBaseMoved;
        _playerSquad.BattleStarted -= OnBattleStarted;
        _playerSquad.NewCapacitySet -= OnNewCapacitySet;
    }

    public void SetUnit(NPC unit)
    {
        if (unit != null)
        {
            ReadyForBattle?.Invoke();
            _playerSquad.AddWaitingUnit(unit);
            SetNextPosition();
        }
    }

    public void SetNewCountPoint(int count)
    {
        _currentCapacity = count;
        HidePoint();
        CalculateLine();
        CurrentPoint = _totalPoints[_numberOfCurrentPoint];

        if (_numberOfCurrentPoint >= _currentCapacity)
            IsFull = true;
        else
            IsFull = false;

    }

    private void SetNextPosition()
    {
        ++_numberOfCurrentPoint;

        if (_numberOfCurrentPoint >= _currentCapacity)
        {
            IsFull = true;
        }
        else
        {
            CurrentPoint = _totalPoints[_numberOfCurrentPoint];
        }
    }

    private void HidePoint()
    {
        ResetDrawPoint();

        for (int i = _totalPoints.Length - 1; i >= _currentCapacity; i--)
        {
            _totalPoints[i].gameObject.SetActive(false);
        }
    }

    private void CalculateLine()
    {
        _sizeController.SetSize(_currentCapacity);
    }

    private void ResetDrawPoint()
    {
        for (int i = _totalPoints.Length - 1; i >= 0; i--)
        {
            _totalPoints[i].gameObject.SetActive(true);
        }
    }

    private void ResetPoints()
    {
        _numberOfCurrentPoint = 0;
        CurrentPoint = _totalPoints[_numberOfCurrentPoint];
        IsFull = false;
    }

    private void OnBattleStarted()
    {
        ResetPoints();
    }

    private void OnBaseMoved()
    {
        // ResetPoints();
    }

    private void OnNewCapacitySet(Dictionary<PlayerSquadZoneType, int> capacity)
    {
        capacity.TryGetValue(_type, out int value);
        SetNewCountPoint(value);
    }
}

public enum PlayerSquadZoneType
{
    Melee,
    Ranged,
    Heavy,
    Cavalry
}
