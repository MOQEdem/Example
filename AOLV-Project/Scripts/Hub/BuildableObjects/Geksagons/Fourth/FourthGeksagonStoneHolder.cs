using UnityEngine;

public class FourthGeksagonStoneHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new FourthGeksagonlStone();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class FourthGeksagonlStone : Resource
    {
        private const string SaveKey = nameof(FourthGeksagonlStone);

        public FourthGeksagonlStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
