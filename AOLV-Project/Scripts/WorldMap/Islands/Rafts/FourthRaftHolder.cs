public class FourthRaftHolder : IslandHolder
{
    private string _islandName = nameof(FourthRaft);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FourthRaft : Island
{
    private const string _saveKey = nameof(FourthRaft);

    public FourthRaft() : base(_saveKey){}
}
