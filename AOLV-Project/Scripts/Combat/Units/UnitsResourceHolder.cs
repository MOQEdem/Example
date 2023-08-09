
public class UnitsResourceHolder : ResourceHolder
{
    protected override Resource InitResource() => new UnitsResource();
    protected override int InitStartValue() => 0;

    public class UnitsResource : Resource
    {
        private const string SaveKey = nameof(UnitsResource);

        public UnitsResource()
            : base(SaveKey, ResourceType.Iron)
        { }
    }

}
