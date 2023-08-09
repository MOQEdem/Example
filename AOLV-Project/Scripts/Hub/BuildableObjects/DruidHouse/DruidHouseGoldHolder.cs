using UnityEngine;

public class DruidHouseGoldHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DruidHouseGold();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class DruidHouseGold : Resource
    {
        private const string SaveKey = nameof(DruidHouseGold);

        public DruidHouseGold()
            : base(SaveKey, ResourceType.Gold)
        { }
    }

}
