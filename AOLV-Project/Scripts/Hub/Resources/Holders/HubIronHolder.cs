public class HubIronHolder : ResourceHolder
{
    protected override Resource InitResource() => new HubIron();
    protected override int InitStartValue() => 0;

    public class HubIron : Resource
    {
        private const string SaveKey = nameof(HubIron);

        public HubIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }

}
