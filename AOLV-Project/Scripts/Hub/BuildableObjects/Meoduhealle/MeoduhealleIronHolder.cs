using UnityEngine;

public class MeoduhealleIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new MeoduhealleIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class MeoduhealleIron : Resource
    {
        private const string SaveKey = nameof(MeoduhealleIron);

        public MeoduhealleIron()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
