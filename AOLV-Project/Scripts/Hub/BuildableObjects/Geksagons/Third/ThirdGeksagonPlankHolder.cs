using UnityEngine;

public class ThirdGeksagonPlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new ThirdGeksagonlPlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class ThirdGeksagonlPlank : Resource
    {
        private const string SaveKey = nameof(ThirdGeksagonlPlank);

        public ThirdGeksagonlPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
