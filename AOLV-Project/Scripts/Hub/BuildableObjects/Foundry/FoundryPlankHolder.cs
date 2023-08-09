using UnityEngine;

public class FoundryPlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FoundryPlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FoundryPlank : Resource
    {
        private const string SaveKey = nameof(FoundryPlank);

        public FoundryPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
