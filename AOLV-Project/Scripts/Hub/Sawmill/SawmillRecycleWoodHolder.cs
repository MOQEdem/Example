public class SawmillRecyclePlankHolder : ResourceHolder
{
    protected override Resource InitResource() => new SawmillRecyclePlank();

    protected override int InitStartValue() => 0;

    public class SawmillRecyclePlank : Resource
    {
        private const string SaveKey = nameof(SawmillRecyclePlank);

        public SawmillRecyclePlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
