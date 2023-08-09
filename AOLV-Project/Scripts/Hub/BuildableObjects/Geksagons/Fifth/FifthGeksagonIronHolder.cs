using UnityEngine;

public class FifthGeksagonIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FifthGeksagonIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FifthGeksagonIron : Resource
    {
        private const string SaveKey = nameof(FifthGeksagonIron);

        public FifthGeksagonIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
