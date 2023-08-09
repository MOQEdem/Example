using UnityEngine;

public class FifthGeksagonPlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FifthGeksagonlPlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FifthGeksagonlPlank : Resource
    {
        private const string SaveKey = nameof(FifthGeksagonlPlank);

        public FifthGeksagonlPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
