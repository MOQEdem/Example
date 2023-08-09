using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class BotVisibilityRange : MonoBehaviour
{
    private UnitType _priorityTargetType;
    private bool IsTargetDetected = false;
    private Unit _target;

    public Unit Target => _target;
    public bool IsDetected => IsTargetDetected;

    public event UnityAction TargetDetected;
    public event UnityAction TargetLosted;

    private HashSet<Unit> _targetUnits = new HashSet<Unit>();

    private void Awake()
    {
        //_priorityTargetType = GetPriorityTargetType();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Unit target))
        {
            if (IsTargetUnit(target) == false)
                return;
            ReliableOnTriggerExit.NotifyTriggerEnter(collision, gameObject, OnTriggerExit);
            DetectTarget(target);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(collision, gameObject);
        if (collision.TryGetComponent(out Unit target))
        {
            if (IsTargetUnit(target) == false)
                return;
            TryLostTarget(target);
        }
    }

    protected abstract UnitType GetPriorityTargetType(Unit target);
    protected abstract bool IsTargetUnit(Unit unit);

    private void DetectTarget(Unit target)
    {
        IsTargetDetected = true;
        if (_targetUnits.Contains(target) == false && target.Type == GetPriorityTargetType(target))
        {
            _targetUnits.Add(target);
        }

        _target = TryGetTarget(_targetUnits);
        TargetDetected?.Invoke();
    }

    private void TryLostTarget(Unit target)
    {
        if (_targetUnits.Contains(target))
            _targetUnits.Remove(target);
        _target = TryGetTarget(_targetUnits);
        if (_target != null)
        {
            IsTargetDetected = true;
            TargetDetected?.Invoke();
            return;
        }

        TargetLosted?.Invoke();
        IsTargetDetected = false;
    }

    private float GetDistance(Unit unit)
    {
        Vector3 direction = unit.transform.position - transform.position;
        return direction.sqrMagnitude;
    }

    private Unit TryGetTarget(HashSet<Unit> targetsUnits)
    {
        if (targetsUnits.Count > 0)
        {
            var priorytiesTargets = targetsUnits.Where(unit => unit.Type == GetPriorityTargetType(unit));
            if (priorytiesTargets.Count() > 0)
            {
                return GetMinDistanceUnit(priorytiesTargets);
            }

            return GetMinDistanceUnit(targetsUnits);
        }

        return null;
    }

    private Unit GetMinDistanceUnit(IEnumerable<Unit> targets)
    {
        float minDistance = float.MaxValue;
        Unit minDistanceTarget = null;
        foreach (var currentTarget in targets)
        {
            float currentDistance = GetDistance(currentTarget);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                minDistanceTarget = currentTarget;
            }
        }

        return minDistanceTarget;
    }
}