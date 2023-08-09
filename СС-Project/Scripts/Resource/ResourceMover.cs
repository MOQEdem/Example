using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ResourceMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _numberOfTurns = 1;
    [SerializeField] private int _upPosition = 1;
    [SerializeField] private float _animationTime = 1;

    private Coroutine _stacking;
    private Collider _collider;

    public Action<ResourceMover> MoveEnded;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void SetTarget(Vector3 target)
    {
        if (_stacking == null)
        {
            _stacking = StartCoroutine(Stacking(target));
        }
    }

    public void StopAllMovement()
    {
        if (_stacking != null)
        {
            StopCoroutine(_stacking);
            transform.DOKill();
            _stacking = null;
        }
    }

    private IEnumerator Stacking(Vector3 target)
    {
        _collider.enabled = false;

        Vector3 startPosition = transform.localPosition;

        Vector3 upPosition = startPosition + Vector3.up * _upPosition;

        Vector3[] path = new Vector3[] { startPosition, upPosition, target };
        transform.DORotate(new Vector3(360 * _numberOfTurns, 0, 0), _animationTime, RotateMode.FastBeyond360);
        Tween patchMoveTeween = transform.DOLocalPath(path, _animationTime, PathType.CatmullRom).SetEase(_animationCurve);
        yield return patchMoveTeween.WaitForCompletion();

        transform.DOKill();
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), _animationTime / 2);

        _collider.enabled = true;

        _stacking = null;
    }
}
