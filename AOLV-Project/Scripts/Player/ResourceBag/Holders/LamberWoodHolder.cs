public class LamberWoodHolder : ResourceHolder
{
    protected override Resource InitResource() => new LamberWood();
    protected override int InitStartValue() => 0;
    
    public class LamberWood : Resource
    {
        public const string SaveKey = nameof(LamberWood);
        
        public LamberWood() :
            base(SaveKey, ResourceType.Wood)
        {
        }
    }
}

