using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class TNT : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private int _height;
    [SerializeField] private int _numberOfTurns;
    [SerializeField] private int _animationTime;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private AudioSource _explosionSound;
    [SerializeField] private AudioSource _flyingSound;

    private Coroutine _flying;
    private TargetDetector _detector;
    private int _damage;

    public event Action<TNT> Exploded;

    private void Awake()
    {
        _detector = GetComponentInChildren<TargetDetector>();
        _explosionSound = GetComponentInChildren<AudioSource>();
    }

    public void Launch(Vector3 point, int damage)
    {
        _damage = damage;

        if (_flying == null)
            _flying = StartCoroutine(Flying(point));
    }

    private IEnumerator Flying(Vector3 point)
    {
        transform.parent = null;
        transform.localScale = Vector3.one;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = point;
        Vector3 upPosition = ((endPosition - startPosition) / 2) + startPosition + (Vector3.up * _height);

        _flyingSound.Play();

        Vector3[] path = new Vector3[] { startPosition, upPosition, endPosition };
        transform.DORotate(new Vector3(77 * _numberOfTurns, 155 * _numberOfTurns, 401 * _numberOfTurns), _animationTime, RotateMode.FastBeyond360);
        Tween patchMoveTeween = transform.DOPath(path, _animationTime, PathType.CatmullRom).SetEase(_animationCurve);
        yield return patchMoveTeween.WaitForCompletion();

        _flyingSound.Stop();

        Character[] targets = _detector.Targets.ToArray();

        _explosionParticle.transform.parent = null;
        _explosionParticle.transform.rotation = Quaternion.Euler(Vector3.zero);
        _explosionParticle.Play();
        _explosionSound.Play();

        foreach (var target in targets)
        {
            if (!target.IsDead)
            {
                target.ApplyDamage(_damage);

                if (target is NPCWalker walker)
                    walker.DoHitJump();
            }
        }

        Exploded?.Invoke(this);

        Destroy(this.gameObject, 0.1f);
        _flying = null;
    }
}
