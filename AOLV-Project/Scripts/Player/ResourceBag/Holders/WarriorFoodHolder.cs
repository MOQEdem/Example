
public class WarriorFoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new WarriorFood();
    protected override int InitStartValue() => 0;

    public class WarriorFood : Resource
    {
        public const string SaveKey = nameof(WarriorFood);

        public WarriorFood() :
            base(SaveKey, ResourceType.Food)
        {
        }
    }
}

