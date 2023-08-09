using UnityEngine;

public class BarnPlankHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;
    
    protected override Resource InitResource() => new BarnPlank();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class BarnPlank : Resource
    {
        private const string SaveKey = nameof(BarnPlank);

        public BarnPlank() : base(SaveKey, ResourceType.Plank){}
    }
}
