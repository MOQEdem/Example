using UnityEngine;

public class SecondGeksagonStoneHolder : ResourceHolder
{
    [SerializeField] private int _resourceValueToSpend;

    protected override Resource InitResource() => new SecondGeksagonlStone();

    protected override int InitStartValue() => _resourceValueToSpend;

    public class SecondGeksagonlStone : Resource
    {
        private const string SaveKey = nameof(SecondGeksagonlStone);

        public SecondGeksagonlStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
