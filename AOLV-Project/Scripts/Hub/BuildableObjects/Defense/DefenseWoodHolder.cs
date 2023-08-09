using UnityEngine;

public class DefenseWoodHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DefenseWood();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class DefenseWood : Resource
    {
        private const string SaveKey = nameof(DefenseWood);

        public DefenseWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
