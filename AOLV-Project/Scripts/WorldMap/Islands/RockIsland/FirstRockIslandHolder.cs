public class FirstRockIslandHolder : IslandHolder
{
    private string _islandName = nameof(FirstRockIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FirstRockIsland : Island
{
    private const string _saveKey = nameof(FirstRockIsland);
    public FirstRockIsland() : base(_saveKey){}
}
