using UnityEngine;

public class PierForStonePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new PierForStonePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class PierForStonePlank : Resource
    {
        private const string SaveKey = nameof(PierForStonePlank);

        public PierForStonePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
