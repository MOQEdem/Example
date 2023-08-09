using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ResourceAnimation : MonoBehaviour
{
    [SerializeField] private float _upMoveRotationYSpeed = 360f;
    [SerializeField] private float _upMoveRotationXSpeed = 360f;
    //[SerializeField] private float _upMoveSpeed = 8f;
    // [SerializeField] private float _horizontalMoveRotationSpeed = 20;
    // [SerializeField] private float _horizontalMoveSpeed = 15;
    [SerializeField] private AnimationCurve _takingCurve;
    [SerializeField] private AnimationCurve _transitionCurve;

    private Coroutine _playingAnimation;

    public event Action Completed;

    public void TryStartAnimation(Transform target, bool isExplosionAnimation)
    {
        if (_playingAnimation != null)
        {
            StopCoroutine(_playingAnimation);
            _playingAnimation = null;
        }

        if (isExplosionAnimation)
            _playingAnimation = StartCoroutine(WaitDOTweenExplosionAnimation(target));
        else
            _playingAnimation = StartCoroutine(WaitDOTweenTransitionAnimation(target));
    }

    private IEnumerator WaitDOTweenExplosionAnimation(Transform target)
    {
        Vector3 point1 = transform.position + GetRandomPoint();
        float randomHorizonalOffset = Random.Range(0.15f, 0.25f);
        Vector3 point2 = Vector3.Lerp(point1, target.position, 0.2f) + Vector3.up;
        transform.rotation = Random.rotation;
        Vector3[] path = new Vector3[] { transform.position, point1, point2, target.position - Vector3.up };
        Tween patchMoveTeween = transform.DOPath(path, 0.7f, PathType.CatmullRom).SetEase(_takingCurve);
        transform.DORotate(new Vector3(Random.Range(90, _upMoveRotationXSpeed), Random.Range(90, _upMoveRotationYSpeed), 0), 0.5f);
        yield return new WaitForSeconds(0.6f);
        patchMoveTeween.Complete();
        transform.DOMove(target.position - Vector3.up, 0.07f).SetEase(Ease.Linear).onComplete += CallCompletedInvoke;
    }

    private IEnumerator WaitDOTweenTransitionAnimation(Transform target)
    {
        Vector3 lerpPoint = Vector3.Lerp(transform.position, target.position, 0.4f) + Vector3.up * 2;
        Vector3 point1 = lerpPoint + Random.insideUnitSphere * 2.5f;
        transform.rotation = Random.rotation;
        Vector3[] path = new Vector3[] { transform.position, point1, target.position };
        transform.DOPath(path, 0.5f, PathType.CatmullRom).SetEase(_transitionCurve).onComplete += CallCompletedInvoke; ;
        transform.DORotate(new Vector3(Random.Range(90, _upMoveRotationXSpeed), Random.Range(90, _upMoveRotationYSpeed), 0), 0.5f);
        yield return null;
    }

    private Vector3 GetRandomPoint()
    {
        float randomXAxis = Random.Range(1f, 2f) * GetRandomSign();
        float randomYAxis = Random.Range(1.5f, 2f);
        float randomZAxis = Random.Range(1f, 2f) * GetRandomSign();
        return new Vector3(randomXAxis, randomYAxis, randomZAxis);
    }

    private int GetRandomSign()
    {
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
            return -1;
        return 1;
    }

    private void CallCompletedInvoke()
    {
        this.DOKill();
        Completed?.Invoke();
    }
}
