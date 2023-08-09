using UnityEngine;

public class ForgeIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new ForgeIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class ForgeIron : Resource
    {
        private const string SaveKey = nameof(ForgeIron);

        public ForgeIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
