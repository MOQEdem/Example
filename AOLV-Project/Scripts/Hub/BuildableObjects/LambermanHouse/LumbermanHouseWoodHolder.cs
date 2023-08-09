using UnityEngine;

public class LumbermanHouseWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new LumbermanHouseWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class LumbermanHouseWood : Resource
    {
        private const string SaveKey = nameof(LumbermanHouseWood);

        public LumbermanHouseWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
