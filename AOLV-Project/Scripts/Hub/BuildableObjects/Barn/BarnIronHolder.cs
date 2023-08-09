using UnityEngine;

public class BarnIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new BarnIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class BarnIron : Resource
    {
        private const string SaveKey = nameof(BarnIron);

        public BarnIron() : base(SaveKey, ResourceType.Iron)
        {
        }
    }
}