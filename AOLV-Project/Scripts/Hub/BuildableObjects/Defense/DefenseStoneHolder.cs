using UnityEngine;

public class DefenseStoneHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DefenseStone();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class DefenseStone : Resource
    {
        private const string SaveKey = nameof(DefenseStone);

        public DefenseStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
