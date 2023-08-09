using UnityEngine;
public class ResourceSource : Unit
{
    [SerializeField] private UnitType _unitType;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _maxResourceCount;
    [SerializeField] private Transform _spawnPoint;

    public ResourceType ResourceType => _resourceType;

    public void SetResourceType(ResourceType spawnedResourceType)
    {
        _resourceType = spawnedResourceType;
    }
    protected override void Die()
    {
    }

    protected override UnitType GetUnitType() => _unitType;

    protected override void TakeDamage(AttackData attackData)
    {
        ReduceHealth(attackData.Weapon);
        CallEventHited(attackData);
        TransferResources(attackData.Unit);
    }

    private void TransferResources(Unit unit)
    {
        int resourceCount = (int)(_maxResourceCount * (Health.LastAppliedDamage / Health.MaxHealth));
        unit.ResourceBag.IsAbleToTakeResource(new ResourcePack(resourceCount, _resourceType), out int possibleToAdd);
        int resourceToAdd = Mathf.Clamp(resourceCount, 0, possibleToAdd);
        unit.ResourceBag.TakeResource(new ResourcePack(resourceToAdd, _resourceType), _spawnPoint);
    }
}
