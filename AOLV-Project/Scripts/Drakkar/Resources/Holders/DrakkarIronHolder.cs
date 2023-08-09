public class DrakkarIronHolder : ResourceHolder
{
    protected override Resource InitResource() => new DrakkarIron();
    protected override int InitStartValue() => 0;

    public class DrakkarIron : Resource
    {
        private const string SaveKey = nameof(DrakkarIron);

        public DrakkarIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }

}
