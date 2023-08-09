using UnityEngine;

public class ThirdGeksagonWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new ThirdGeksagonlWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class ThirdGeksagonlWood : Resource
    {
        private const string SaveKey = nameof(ThirdGeksagonlWood);

        public ThirdGeksagonlWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
