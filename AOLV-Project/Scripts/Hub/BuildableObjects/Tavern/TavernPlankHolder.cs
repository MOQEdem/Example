using UnityEngine;

public class TavernPlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new TavernPlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class TavernPlank : Resource
    {
        private const string SaveKey = nameof(TavernPlank);

        public TavernPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
