using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _moveDuration = 1;
    [SerializeField] [Min(0)] private float _startYOffset = 10;
    [SerializeField] [Min(0)] private float _maxDelay = 1;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private ParticleSystem _explode;

    private void Awake() => 
        transform.position += new Vector3(0, _startYOffset, 0);

    public void Move(Action endMoveCallback) =>
        transform.DOMoveY(0, _moveDuration)
            .SetDelay(Random.Range(0, _maxDelay))
            .SetEase(_moveCurve)
            .OnComplete(() =>
            {
                endMoveCallback?.Invoke();
                _explode.transform.parent = null;
                _explode.Play();
                Destroy(gameObject);
            });
}
