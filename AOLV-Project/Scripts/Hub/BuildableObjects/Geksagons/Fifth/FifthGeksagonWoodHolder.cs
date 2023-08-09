using UnityEngine;

public class FifthGeksagonWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FifthGeksagonlWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FifthGeksagonlWood : Resource
    {
        private const string SaveKey = nameof(FifthGeksagonlWood);

        public FifthGeksagonlWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
