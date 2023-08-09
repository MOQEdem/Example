using UnityEngine;

public class MinerHousePlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new MinerHousePlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class MinerHousePlank : Resource
    {
        private const string SaveKey = nameof(MinerHousePlank);

        public MinerHousePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
