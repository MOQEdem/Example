using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class BaseExtender : MonoBehaviour
{
    [SerializeField] private CornerPoint _meleePoint;
    [SerializeField] private CornerPoint _rangedPoint;
    [SerializeField] private PlayerSquadZone _rangedZone;
    [SerializeField] private BaseBuildings _buildings;

    private PlayerSquad _playerSquad;
    private Vector3 _rangedZoneOffset;
    private Vector3 _buildingsZoneOffset;
    private Coroutine _movingBase;

    public event Action BaseMoved;

    private void Awake()
    {
        _playerSquad = GetComponentInChildren<PlayerSquad>();
        _rangedZoneOffset = _rangedZone.transform.position - _meleePoint.transform.position;
        _buildingsZoneOffset = _buildings.transform.position - _rangedPoint.transform.position;
    }

    private void OnEnable()
    {
        _playerSquad.NewCapacitySet += OnNewCapacitySet;
    }

    private void OnDisable()
    {
        _playerSquad.NewCapacitySet -= OnNewCapacitySet;
    }

    private void OnNewCapacitySet(Dictionary<PlayerSquadZoneType, int> capacity)
    {
        if (_movingBase != null)
        {
            StopCoroutine(_movingBase);
            _movingBase = null;
        }

        _movingBase = StartCoroutine(MovingBase());
    }

    private IEnumerator MovingBase()
    {
        var delay = new WaitForSeconds(0.25f);
        float movingTime = 0.3f;

        yield return delay;

        Tween moving = _rangedZone.transform.DOMove(_meleePoint.transform.position + _rangedZoneOffset, movingTime);
        yield return moving.WaitForCompletion();

        moving = _buildings.transform.DOMove(_rangedPoint.transform.position + _buildingsZoneOffset, movingTime);
        yield return moving.WaitForCompletion();

        BaseMoved?.Invoke();
        _movingBase = null;
    }
}
