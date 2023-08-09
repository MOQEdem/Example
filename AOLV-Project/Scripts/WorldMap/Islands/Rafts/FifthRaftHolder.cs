public class FifthRaftHolder : IslandHolder
{
    private string _islandName = nameof(FifthRaft);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FifthRaft : Island
{
    private const string _saveKey = nameof(FifthRaft);

    public FifthRaft() : base(_saveKey){}
}
