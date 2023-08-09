public class PlayerPlankHolder : ResourceHolder
{
    protected override Resource InitResource() => new PlayerPlank();
    protected override int InitStartValue() => 0;

    public class PlayerPlank : Resource
    {
        private const string SaveKey = nameof(PlayerPlank);

        public PlayerPlank()
            : base(SaveKey, ResourceType.Plank)
        { }
    }
}
