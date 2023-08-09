public class DrakkarWoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new DrakkarWood();
    protected override int InitStartValue() => 0;

    public class DrakkarWood : Resource
    {
        private const string SaveKey = nameof(DrakkarWood);

        public DrakkarWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }

}
