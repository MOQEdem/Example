using UnityEngine;

public class DefensePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DefensePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class DefensePlank : Resource
    {
        private const string SaveKey = nameof(DefensePlank);

        public DefensePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
