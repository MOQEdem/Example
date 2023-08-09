using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MageBossRange : EnemyNPC
{
    [Space]
    [Header("ArcherStaff")]
    [SerializeField] private List<Arrow> _arrowsPool;

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

    protected override void DoAttack()
    {
        gameObject.transform.DOLookAt(Target.transform.position, 0.2f);

        Attack();

        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);
        LastAttackTime = AttackDelay;
    }

    protected override void Attack()
    {
        Animator.Attack();
        Audio.Attack();
    }

    public void Shoot()
    {
        Arrow arrow = _arrowsPool.First(a => a.gameObject.activeSelf == false);

        Animator.Attack();
        Audio.Attack();

        arrow.Shoot(Target, Damage);
    }

    private void OnTargetHit()
    {

    }
}
