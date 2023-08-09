using UnityEngine;

public class EnemyEffectsStarter : UnitEffectsStarter
{
    [SerializeField] private RagdollActivator _ragdollActivator;

    protected override void OnUnitDied(Unit unit)
    {
        _ragdollActivator.Activate(LastAttackPoint);
    }

    protected override void OnUnitAttacked(AttackData attackData)
    {
        PlayDamageParticles();
        SetLastAttackPoint(attackData);
        ShowPopUpText();
    }
}
