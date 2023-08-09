public class DrakkarGoldHolder : ResourceHolder
{
    protected override Resource InitResource() => new DrakkarGold();
    protected override int InitStartValue() => 0;

    public class DrakkarGold : Resource
    {
        private const string SaveKey = nameof(DrakkarGold);

        public DrakkarGold()
            : base(SaveKey, ResourceType.Gold)
        { }
    }

}
