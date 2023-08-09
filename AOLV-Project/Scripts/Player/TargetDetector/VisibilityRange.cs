using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VisibilityRange : MonoBehaviour
{
    // [SerializeField] private float _timeBetweenChecks = 0.1f;
    [SerializeField] private float _searchRadius = 0.2f;
    [SerializeField] private float _visibilityAngle = 90;
    [SerializeField] private float _detectingDelay = 0.1f;
    [SerializeField] private LayerMask _targetLayer;

    private bool _isTargetsNearby = false;
    private Coroutine _waitDetectTarget;
    private Vector3 _targetPosition = Vector3.zero;
    private Transform _closestTarget;

    public bool IsTargetsNearby => _isTargetsNearby;
    public Vector3 TargetPosition => _targetPosition;
    public Transform ClosestTarget => _closestTarget;

    public event UnityAction<Unit> TargetDetected;
    public event UnityAction TargetLossed;


    private void OnEnable()
    {
        _waitDetectTarget = StartCoroutine(WaitDetectTarget());
    }

    private void OnDisable()
    {
        StopCoroutine(_waitDetectTarget);
    }

    private IEnumerator WaitDetectTarget()
    {
        WaitForSeconds forSeconds = new WaitForSeconds(_detectingDelay);

        while (true)
        {
            TryTargetsDetect();
            yield return forSeconds;
        }
    }

    private void TryTargetsDetect()
    {

        var targetsColliders = Physics.OverlapSphere(transform.position, _searchRadius, _targetLayer);
        if (targetsColliders.Length > 0)
        {
            DetectTargets(targetsColliders);
        }
        else
        {
            LoseTarget();
        }
    }

    private void DetectTargets(Collider[] targetsColliders)
    {
        Collider targetCollider = GetTargetCollider(targetsColliders);
        if (targetCollider == null)
        {
            LoseTarget();
            return;
        }
        _closestTarget = targetCollider.transform;
        _isTargetsNearby = true;
        Unit targetUnit = targetCollider.GetComponent<Unit>();
        if (targetUnit == null)
            throw new NullReferenceException("Not set Unit Component in target Layer");
        TargetDetected?.Invoke(targetUnit);
    }

    private Collider GetTargetCollider(Collider[] targetsColliders)
    {
        float minDistance = float.MaxValue;
        Collider targetCollider = null;

        foreach (var collider in targetsColliders)
        {
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            float currentAngle = Vector3.Angle(transform.forward, direction);
            if (currentAngle > _visibilityAngle)
                continue;
            float currentDistance = Vector3.Distance(transform.position, collider.transform.position);
            if (currentDistance > minDistance)
                continue;
            minDistance = currentDistance;
            targetCollider = collider;
        }
        return targetCollider;
    }

    private void LoseTarget()
    {
        _isTargetsNearby = false;
        TargetLossed?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }
}
