using UnityEngine;

public class ForgePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new ForgePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class ForgePlank : Resource
    {
        private const string SaveKey = nameof(ForgePlank);

        public ForgePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
