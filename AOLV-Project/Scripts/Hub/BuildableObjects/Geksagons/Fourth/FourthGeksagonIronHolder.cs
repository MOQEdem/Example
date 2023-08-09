using UnityEngine;

public class FourthGeksagonIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FourthGeksagonIron();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FourthGeksagonIron : Resource
    {
        private const string SaveKey = nameof(FourthGeksagonIron);

        public FourthGeksagonIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
