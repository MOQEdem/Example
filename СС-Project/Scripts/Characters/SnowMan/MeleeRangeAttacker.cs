using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MeleeRangeAttacker : EnemyNPC
{
    [Space]
    [Header("ArcherStaff")]
    [SerializeField][Min(0)] private int _rangeAttackDamage = 20;
    [SerializeField] private float _meleeAttackRadius = 1f;
    [SerializeField] private float _rangeAttackRadius = 7f;
    [SerializeField] private List<Arrow> _arrowsPool;

    private bool _isRangeAttack = true;

    protected override void Start()
    {
        base.Start();
        Animator.SetRangeAttack(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (var arrow in _arrowsPool)
            arrow.TargetHit += OnTargetHit;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        foreach (var arrow in _arrowsPool)
            arrow.TargetHit -= OnTargetHit;
    }

    private void Update()
    {
        if (Target && Vector3.Distance(transform.position, Target.transform.position) < _meleeAttackRadius)
        {
            if (_isRangeAttack)
            {
                _isRangeAttack = false;
                AttackRange = _meleeAttackRadius;
                Animator.SetRangeAttack(false);
            }
        }
        else
        {
            if (!_isRangeAttack)
            {
                _isRangeAttack = true;
                AttackRange = _rangeAttackRadius;
                Animator.SetRangeAttack(true);
            }
        }
    }

    protected override void DoAttack()
    {
        if (_isRangeAttack)
            RangeAttack();
        else
            MeleeAttack();
    }

    private void RangeAttack()
    {
        gameObject.transform.DOLookAt(Target.transform.position, 0.2f);
        Arrow arrow = _arrowsPool.First(a => a.gameObject.activeSelf == false);

        Animator.Attack();
        Audio.Attack();

        arrow.Shoot(Target, _rangeAttackDamage);

        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);
        LastAttackTime = AttackDelay;
    }

    private void MeleeAttack()
    {
        gameObject.transform.DOLookAt(Target.transform.position, 0.2f);

        Attack();

        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);
        LastAttackTime = AttackDelay;
    }

    private void OnTargetHit()
    {

    }
}
