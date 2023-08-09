using UnityEngine;

public class MinerHouseIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new MinerHouseIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class MinerHouseIron : Resource
    {
        private const string SaveKey = nameof(MinerHouseIron);

        public MinerHouseIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
