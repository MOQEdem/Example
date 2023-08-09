public class PlayerIronHolder : ResourceHolder
{
    protected override Resource InitResource() => new PlayerIron();
    protected override int InitStartValue() => 0;

    public class PlayerIron : Resource
    {
        private const string SaveKey = nameof(PlayerIron);

        public PlayerIron()
            : base(SaveKey, ResourceType.Iron)
        { }
    }

}
