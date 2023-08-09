using UnityEngine;

public class DefenseIronHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new DefenseIron();

    protected override int InitStartValue() => _resourceValueToSpend;


    public class DefenseIron : Resource
    {
        private const string SaveKey = nameof(DefenseIron);

        public DefenseIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
