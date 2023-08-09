using UnityEngine;

public class DruidHouseIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DruidHouseIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class DruidHouseIron : Resource
    {
        private const string SaveKey = nameof(DruidHouseIron);

        public DruidHouseIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
