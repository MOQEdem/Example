using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRider : NPCRider
{
    private float sqrAttackRange;

    protected override void Awake()
    {
        base.Awake();

        sqrAttackRange = AttackRange * AttackRange;
    }

    protected virtual void FixedUpdate()
    {
        if (!IsDead)
        {
            CheckTargetDistance();

            if (Target != null && IsAttack)
            {
                if (Target.IsDead == false)
                {
                    TryToAttack();
                }

                if (Target.IsDead)
                {
                    TrySetNextTarget();
                }
            }

            if (LastAttackTime > 0)
                LastAttackTime -= Time.fixedDeltaTime;
        }
    }

    private void TryToAttack()
    {
        if ((transform.position - Target.transform.position).sqrMagnitude < sqrAttackRange)
        {
            if (LastAttackTime <= 0)
            {
                DoAttack();
            }
        }
    }

    protected virtual void DoAttack()
    {
        Attack();
        LastAttackTime = AttackDelay;
    }

    private void TrySetNextTarget()
    {
        Target = TryGetNextTarget();
    }
}
