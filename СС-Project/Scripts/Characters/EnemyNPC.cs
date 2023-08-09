using UnityEngine;

public abstract class EnemyNPC : NPCWalker
{
    private NPCRandomPositionSetter _npcRandomPositionSetter;

    protected override void Start()
    {
        DefaultPosition = transform.position;
        _npcRandomPositionSetter = GetComponent<NPCRandomPositionSetter>();
    }

    protected virtual void FixedUpdate()
    {
        if (Mover.NavMeshAgent.enabled && !IsDead)
        {
            if (_npcRandomPositionSetter != null && _npcRandomPositionSetter.enabled)
                _npcRandomPositionSetter.TryGetNewDefaultPosition();

            MoveToDefaultPosition();

            Mover.CheckEndPath();

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
        if ((transform.position - Target.transform.position).sqrMagnitude < AttackRange * AttackRange)
        {
            if (LastAttackTime <= 0)
            {
                DoAttack();
            }
        }
        else
        {
            Animator.Run(true);
            Mover.SetSpeedMove(Mover.Speed);
            Mover.SetGoalToMove(Target.transform.position);
        }
    }

    protected virtual void DoAttack()
    {
        Attack();
        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);
        LastAttackTime = AttackDelay;
    }

    private void TrySetNextTarget()
    {
        Target = TryGetNextTarget();
        Mover.SetSpeedMove(Mover.Speed);

        if (Target != null)
        {
            SetTargetPosition(Target.transform.position);
        }
        else
        {
            Mover.SetGoalToMove(DefaultPosition);
            Mover.NavMeshAgent.stoppingDistance = 0;
        }
    }
}
