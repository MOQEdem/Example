using UnityEngine;

public class HitState : AttackState
{
    [SerializeField] private BotSwordHitter _swordHitter;    
   
    protected override void AttackTarget(Unit unit)
    {
        _swordHitter.HitEnded += OnHitEneded;        
        _swordHitter.BaseAttack();
    }

    private void OnHitEneded()
    {
        _swordHitter.HitEnded -= OnHitEneded;
        StartReloadAttack();
    }   
}
