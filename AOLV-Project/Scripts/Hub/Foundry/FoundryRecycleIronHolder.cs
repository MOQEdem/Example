public class FoundryRecycleStoneHolder : ResourceHolder
{
    protected override Resource InitResource() => new FoundryRecycleStone();

    protected override int InitStartValue() => 0;

    public class FoundryRecycleStone : Resource
    {
        private const string SaveKey = nameof(FoundryRecycleStone);

        public FoundryRecycleStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
