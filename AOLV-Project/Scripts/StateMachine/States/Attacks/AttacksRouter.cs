using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BaseTimer))]
public class AttacksRouter : State
{
    [SerializeField] private float _minLongAttackDistance = 5f;
    [SerializeField] private HitState _hitState;
    [SerializeField] private PursuitState _pursuitState;
    [SerializeField] private ShootState _shootState;

    private State _nextState;
    private EnemyVisibilityRange _enemyVisibilityRange;
    private BotAttackStarter _attackStarter;
    private BaseTimer _attackTimer;
    private Player _target;

    public State NextState => _nextState;

    public event UnityAction<State> Routed;

    private void Awake()
    {       
        _attackTimer = GetComponent<BaseTimer>();
        _attackStarter = GetComponentInChildren<BotAttackStarter>();
        _enemyVisibilityRange = GetComponentInChildren<EnemyVisibilityRange>();
    }

    //private void OnEnable()
    //{
    //    _target = _enemyVisibilityRange.Target;      
    //    RouteAttack();
    //}

    private void OnDisable()
    {
        _nextState = null;
    }

    private void RouteAttack()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _minLongAttackDistance)
        {
            _nextState = _hitState;
        }
        else
        {
            _nextState = GetRandomState();
        }
        StartCoroutine(WaitRotateBeforeAttack());
    }

    private void RotateToTarget(Transform target, float rotationTime)
    {       
        float offsetTime = 0.1f;
        rotationTime -= offsetTime;
        Vector3 shootTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = shootTarget - transform.position;
        Quaternion targetQuternion = Quaternion.LookRotation(direction, transform.up);
        transform.DORotate(targetQuternion.eulerAngles, rotationTime);
    }

    private IEnumerator WaitRotateBeforeAttack()
    {
        float rotationTime = 0.5f;
        RotateToTarget(_target.transform, rotationTime);
        yield return new WaitForSeconds(rotationTime);
        Routed?.Invoke(_nextState);        
    }

    private State GetRandomState()
    {
        int minIndex = 0;
        int maxIndex = 2;
        int randomIndex = Random.Range(minIndex, maxIndex);    
        switch (randomIndex)
        {
            case 0:
                return _pursuitState;
            case 1:
                return _shootState;
            default:
                throw new ArgumentException();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minLongAttackDistance);
    }
}
