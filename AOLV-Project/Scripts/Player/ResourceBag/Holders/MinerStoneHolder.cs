public class MinerStoneHolder : ResourceHolder
{
    protected override Resource InitResource() => new MinerStone();
    protected override int InitStartValue() => 0;

    public class MinerStone : Resource
    {
        public const string SaveKey = nameof(MinerStone);

        public MinerStone() :
            base(SaveKey, ResourceType.Stone)
        {
        }
    }
}