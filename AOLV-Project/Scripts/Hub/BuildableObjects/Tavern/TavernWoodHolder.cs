using UnityEngine;

public class TavernWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new TavernWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class TavernWood : Resource
    {
        private const string SaveKey = nameof(TavernWood);

        public TavernWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
