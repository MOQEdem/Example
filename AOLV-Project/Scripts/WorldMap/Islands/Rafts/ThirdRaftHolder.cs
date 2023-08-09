public class ThirdRaftHolder : IslandHolder
{
    private string _islandName = nameof(ThirdRaft);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class ThirdRaft : Island
{
    private const string _saveKey = nameof(ThirdRaft);

    public ThirdRaft() : base(_saveKey){}
}
