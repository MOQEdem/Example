using UnityEngine;

public class AlliedNPC : NPCWalker
{
    [Space]
    [Header("Buff Options")]
    [SerializeField] private bool _isHeavy;
    [SerializeField] private ParticleSystem _buffParticle;

    private Transform _startPoint;
    private bool _isForgeBuffed = false;

    public bool IsBuffed => _isForgeBuffed;
    public bool IsHeavy => _isHeavy;

    private void FixedUpdate()
    {
        if (Mover.NavMeshAgent.enabled)
        {
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

            if (IsBattleEnded && !Mover.IsMove)
            {
                TryPlayVictoryAnimation();
            }

            if (LastAttackTime > 0)
                LastAttackTime -= Time.fixedDeltaTime;
        }
    }

    public void SetAttackStatus(bool isAttack)
    {
        IsAttack = isAttack;
    }

    public void SetStartPoint(Transform transform)
    {
        _startPoint = transform;
    }

    public void TeleportToStartPoint()
    {
        Mover.NavMeshAgent.enabled = false;
        transform.position = _startPoint.transform.position;
        Mover.NavMeshAgent.enabled = true;
        SetDefaultPosition(_startPoint.transform.position);
        SetTargetPosition(_startPoint.transform.position);
    }

    public void ResetStartPosition()
    {
        if (!IsAttack)
        {
            DefaultPosition = _startPoint.transform.position;
            SetTargetPosition(DefaultPosition);
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

    private void TryPlayVictoryAnimation()
    {
        if (IsAttack)
        {
            Mover.NavMeshAgent.stoppingDistance = 0.5f;
            IsAttack = false;
            Animator.Victory();
        }

        transform.LookAt(Player.transform.position);
    }

    protected override void Die()
    {
        base.Die();

        if (_buffParticle != null)
            _buffParticle.gameObject.SetActive(false);
    }

    public void SetBuff(bool isBuff, int helth, int damage, float scale)
    {
        if (_buffParticle != null)
            _buffParticle.gameObject.SetActive(isBuff);

        ChangeSkill(helth, damage, scale);
    }

    public void SetForgeBuff()
    {
        _isForgeBuffed = true;
    }
}
