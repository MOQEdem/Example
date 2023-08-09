public class HubWoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new HubWood();
    protected override int InitStartValue() => 0;

    public class HubWood : Resource
    {
        private const string SaveKey = nameof(HubWood);

        public HubWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }

}
