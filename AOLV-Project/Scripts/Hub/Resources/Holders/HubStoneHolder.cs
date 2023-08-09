public class HubStoneHolder : ResourceHolder
{
    protected override Resource InitResource() => new HubStone();
    protected override int InitStartValue() => 0;

    public class HubStone : Resource
    {
        private const string SaveKey = nameof(HubStone);

        public HubStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
