public class PlayerFoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new PlayerFood();
    protected override int InitStartValue() => 0;

    public class PlayerFood : Resource
    {
        private const string SaveKey = nameof(PlayerFood);

        public PlayerFood()
            : base(SaveKey, ResourceType.Food)
        { }
    }
}
