using System;
using UnityEngine;

public abstract class Bot : Unit
{
    [SerializeField] private Player _target;
    [SerializeField] private BotSwordHitter _botSwordHitter;
    [SerializeField] private bool _isInvulnerable = false;
    [SerializeField] private NPCType _npcType;


    public event Action Captured;
    public event Action<float> Freed;

    public Player Target => _target;
    public NPCType NPCType => _npcType;

    protected override void TakeDamage(AttackData attackData)
    {
        CallEventPushed(attackData.AttackPosition);
        if (_isInvulnerable == true)
            return;
        ReduceHealth(attackData.Weapon);
        CallEventHited(attackData);
    }

    public Rigidbody GetRigidbody()
    {
        throw new NotImplementedException();
    }

    public void SetPlayer(Player player)
    {
        _target = player;
    }
    protected abstract UnitType GetBotUnitType();

    protected override void Die()
    {
        StartWaitingDestroy();
    }

    protected void TrySetTarget(Unit unit)
    {
        if (_target != null)
            return;
        if (unit is Player player)
        {
            _target = player;
        }
        else
        {
            throw new Exception("Not valid unit");
        }
    }

    protected override UnitType GetUnitType()
    {
        return GetBotUnitType();
    }


    protected void InitWeapon()
    {
        _botSwordHitter.SetTargetUnit(_target);
    }
}
