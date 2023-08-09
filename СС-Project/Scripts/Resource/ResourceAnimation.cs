using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ResourceAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _numberOfTurns = 1;
    [SerializeField] private int _upPosition = 1;
    [SerializeField] private float _animationTime = 1;

    private Coroutine _dropping;
    private Collider _collider;
    private float _heightOffset = 0.3f;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void PlayDropAnimation(float height)
    {
        if (_dropping == null)
        {
            _dropping = StartCoroutine(Dropping(height));
        }
    }

    private IEnumerator Dropping(float height)
    {
        _collider.enabled = false;

        Vector3 startPosition = transform.position;
        startPosition.y = height + _heightOffset;
        transform.position = startPosition;
        Vector3 upPosition = startPosition + Vector3.up * _upPosition;

        Vector3[] path = new Vector3[] { startPosition, upPosition, startPosition };
        transform.DORotate(new Vector3(0, 0, 360 * _numberOfTurns), _animationTime, RotateMode.FastBeyond360);
        Tween patchMoveTeween = transform.DOPath(path, _animationTime, PathType.CatmullRom).SetEase(_animationCurve);
        yield return patchMoveTeween.WaitForCompletion();

        _collider.enabled = true;

        _dropping = null;
    }
}
