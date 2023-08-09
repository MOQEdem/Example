using UnityEngine;

public class Enemy : Bot
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _maxResourceCount;
    [SerializeField] private Transform _spawnPoint;

    protected override UnitType GetBotUnitType()
    {
        return UnitType.Enemy;
    }

    protected override void TakeDamage(AttackData attackData)
    {
        TrySetTarget(attackData.Unit);
        CallEventPushed(attackData.AttackPosition);
        ReduceHealth(attackData.Weapon);
        CallEventHited(attackData);
    }

    protected override void Die()
    {
        TransferResources(Target);
        StartWaitingDestroy();
    }

    private void TransferResources(Unit unit)
    {
        unit.ResourceBag.IsAbleToTakeResource(new ResourcePack(_maxResourceCount, _resourceType), out int possibleToAdd);
        int resourceToAdd = Mathf.Clamp(_maxResourceCount, 0, possibleToAdd);
        unit.ResourceBag.TakeResource(new ResourcePack(resourceToAdd, _resourceType), _spawnPoint);
    }
}
