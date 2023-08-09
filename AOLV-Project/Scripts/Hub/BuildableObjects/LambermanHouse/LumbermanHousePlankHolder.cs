using UnityEngine;

public class LumbermanHousePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new LumbermanHousePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class LumbermanHousePlank : Resource
    {
        private const string SaveKey = nameof(LumbermanHousePlank);

        public LumbermanHousePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
