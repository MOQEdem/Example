public class PlayerStoneHolder : ResourceHolder
{
    protected override Resource InitResource() => new PlayerStone();
    protected override int InitStartValue() => 0;

    public class PlayerStone : Resource
    {
        private const string SaveKey = nameof(PlayerStone);

        public PlayerStone()
            : base(SaveKey, ResourceType.Stone)
        { }
    }
}
