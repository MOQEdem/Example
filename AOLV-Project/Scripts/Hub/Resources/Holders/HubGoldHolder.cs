public class HubGoldHolder : ResourceHolder
{
    protected override Resource InitResource() => new HubGold();
    protected override int InitStartValue() => 0;

    public class HubGold : Resource
    {
        private const string SaveKey = nameof(HubGold);

        public HubGold()
            : base(SaveKey, ResourceType.Gold)
        { }
    }

}
