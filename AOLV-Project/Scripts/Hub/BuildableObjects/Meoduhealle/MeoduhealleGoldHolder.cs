using UnityEngine;

public class MeoduhealleGoldHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new MeoduhealleGold();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class MeoduhealleGold : Resource
    {
        private const string SaveKey = nameof(MeoduhealleGold);

        public MeoduhealleGold()
            : base(SaveKey, ResourceType.Gold)
        { }
    }

}
