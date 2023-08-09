public class HubPlankHolder : ResourceHolder
{
    protected override Resource InitResource() => new HubPlank();
    protected override int InitStartValue() => 0;

    public class HubPlank : Resource
    {
        private const string SaveKey = nameof(HubPlank);

        public HubPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
