public class PlayerWoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new PlayerWood();
    protected override int InitStartValue() => 0;

    public class PlayerWood : Resource
    {
        private const string SaveKey = nameof(PlayerWood);

        public PlayerWood()
            : base(SaveKey, ResourceType.Wood)
        { }
    }

}
