using UnityEngine;

public class FoundryStoneHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FoundryStone();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FoundryStone : Resource
    {
        private const string SaveKey = nameof(FoundryStone);

        public FoundryStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
