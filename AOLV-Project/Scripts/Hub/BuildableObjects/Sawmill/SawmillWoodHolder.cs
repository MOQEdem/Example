using UnityEngine;

public class SawmillWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new SawmillWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class SawmillWood : Resource
    {
        private const string SaveKey = nameof(SawmillWood);

        public SawmillWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
