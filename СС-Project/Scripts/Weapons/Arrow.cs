using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _flyTime;
    [SerializeField] private Transform _shootPosition;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Coroutine _flying;
    private Character _target;

    public event Action TargetHit;

    public float FlyTime => _flyTime;

    private void Start()
    {
        _startPosition = transform.position;
        gameObject.SetActive(false);
    }

    public void Shoot(Character target, int damage)
    {
        if(target == null)
            return;
        
        transform.position = _shootPosition.position;
        this.gameObject.SetActive(true);

        if (_flying == null)
        {
            _target = target;
            _targetPosition = target.transform.position;
            _flying = StartCoroutine(Flying(damage));
        }
    }

    private IEnumerator Flying(int damage)
    {
        Tween fly = transform.DOMove(_targetPosition, _flyTime).SetEase(Ease.Linear);
        yield return fly.WaitForCompletion();

        DoDamage(damage);

        transform.position = _startPosition;

        _flying = null;
        this.gameObject.SetActive(false);
    }

    protected virtual void DoDamage(int damage)
    {
        if (_target && !_target.IsDead)
        {
            _target.ApplyDamage(damage);
            TargetHit?.Invoke();
        }
    }
}
