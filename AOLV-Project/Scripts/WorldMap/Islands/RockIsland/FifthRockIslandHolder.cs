public class FifthRockIslandHolder : IslandHolder
{
    private string _islandName = nameof(FifthRockIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FifthRockIsland : Island
{
    private const string _saveKey = nameof(FifthRockIsland);
    public FifthRockIsland() : base(_saveKey){}
}