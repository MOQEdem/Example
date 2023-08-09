public class DrakkarPlankHolder : ResourceHolder
{
    protected override Resource InitResource() => new DrakkarPlank();
    protected override int InitStartValue() => 0;

    public class DrakkarPlank : Resource
    {
        private const string SaveKey = nameof(DrakkarPlank);

        public DrakkarPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
