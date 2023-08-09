using UnityEngine;

public class DruidHousePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DruidHousePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class DruidHousePlank : Resource
    {
        private const string SaveKey = nameof(DruidHousePlank);

        public DruidHousePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
