using UnityEngine;

public class FifthGeksagonStoneHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FifthGeksagonlStone();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FifthGeksagonlStone : Resource
    {
        private const string SaveKey = nameof(FifthGeksagonlStone);

        public FifthGeksagonlStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
