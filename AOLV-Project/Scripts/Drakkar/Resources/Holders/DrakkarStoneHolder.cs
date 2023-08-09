public class DrakkarStoneHolder : ResourceHolder
{
    protected override Resource InitResource() => new DrakkarStone();
    protected override int InitStartValue() => 0;

    public class DrakkarStone : Resource
    {
        private const string SaveKey = nameof(DrakkarStone);

        public DrakkarStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
