public class ThirdtRockIslandHolder : IslandHolder
{
    private string _islandName = nameof(ThirdtRockIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class ThirdtRockIsland : Island
{
    private const string _saveKey = nameof(ThirdtRockIsland);
    public ThirdtRockIsland() : base(_saveKey){}
}
