using UnityEngine;

public class PierForWoodWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new PierForWoodWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class PierForWoodWood : Resource
    {
        private const string SaveKey = nameof(PierForWoodWood);

        public PierForWoodWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
