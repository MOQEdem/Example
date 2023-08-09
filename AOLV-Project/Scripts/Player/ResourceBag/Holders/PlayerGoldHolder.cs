public class PlayerGoldHolder : ResourceHolder
{
    protected override Resource InitResource() => new PlayerGold();
    protected override int InitStartValue() => 0;

    public class PlayerGold : Resource
    {
        private const string SaveKey = nameof(PlayerGold);

        public PlayerGold()
            : base(SaveKey, ResourceType.Gold)
        { }
    }

}
