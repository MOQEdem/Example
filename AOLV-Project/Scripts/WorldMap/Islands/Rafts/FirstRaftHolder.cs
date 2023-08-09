public class FirstRaftHolder : IslandHolder
{
    private string _islandName = nameof(FirstRaft);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FirstRaft : Island
{
    private const string _saveKey = nameof(FirstRaft);

    public FirstRaft() : base(_saveKey){}
}