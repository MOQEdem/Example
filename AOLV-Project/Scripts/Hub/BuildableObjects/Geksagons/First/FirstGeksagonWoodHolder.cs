using UnityEngine;

public class FirstGeksagonWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FirstGeksagonWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FirstGeksagonWood : Resource
    {
        private const string SaveKey = nameof(FirstGeksagonWood);

        public FirstGeksagonWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
