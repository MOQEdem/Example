using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRiderArcher : EnemyRider
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
        Arrow arrow = _arrowsPool.First(a => a.gameObject.activeSelf == false);

        Animator.Attack();
        Audio.Attack();
        arrow.Shoot(Target, Damage);

        LastAttackTime = AttackDelay;
    }


    private void OnTargetHit()
    {

    }
}
