public class FoundryRecycleIronHolder : ResourceHolder
{
    protected override Resource InitResource() => new FoundryRecycleIron();

    protected override int InitStartValue() => 0;

    public class FoundryRecycleIron : Resource
    {
        private const string SaveKey = nameof(FoundryRecycleIron);

        public FoundryRecycleIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }
}
