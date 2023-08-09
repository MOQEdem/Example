using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class PlayerArcher : AlliedNPC
{
    [Space]
    [Header("Archer Stuff")]
    [SerializeField] protected List<Arrow> ArrowsPool;

    protected override void DoAttack()
    {
        gameObject.transform.DOLookAt(Target.transform.position, 0.2f);
        Arrow arrow = ArrowsPool.First(a => a.gameObject.activeSelf == false);

        Animator.Attack();
        Audio.Attack();

        arrow.Shoot(Target, Damage);

        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);
        LastAttackTime = AttackDelay;
    }
}