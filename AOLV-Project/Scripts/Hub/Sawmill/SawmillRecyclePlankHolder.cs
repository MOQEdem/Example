public class SawmillRecycleWoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new SawmillRecycleWood();

    protected override int InitStartValue() => 0;

    public class SawmillRecycleWood : Resource
    {
        private const string SaveKey = nameof(SawmillRecycleWood);

        public SawmillRecycleWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }
}
