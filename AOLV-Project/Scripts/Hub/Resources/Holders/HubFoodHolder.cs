public class HubFoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new HubFood();
    protected override int InitStartValue() => 0;

    public class HubFood : Resource
    {
        private const string SaveKey = nameof(HubFood);

        public HubFood()
            : base(SaveKey, ResourceType.Food)
        { }
    }

}
