
public class WarriorGoldHolder : ResourceHolder
{
    protected override Resource InitResource() => new WarriorGold();
    protected override int InitStartValue() => 0;

    public class WarriorGold : Resource
    {
        public const string SaveKey = nameof(WarriorGold);

        public WarriorGold() :
            base(SaveKey, ResourceType.Gold)
        {
        }
    }
}

