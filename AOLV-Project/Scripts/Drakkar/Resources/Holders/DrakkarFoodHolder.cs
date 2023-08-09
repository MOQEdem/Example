public class DrakkarFoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new DrakkarFood();
    protected override int InitStartValue() => 0;

    public class DrakkarFood : Resource
    {
        private const string SaveKey = nameof(DrakkarFood);

        public DrakkarFood()
            : base(SaveKey, ResourceType.Food)
        { }
    }
}
