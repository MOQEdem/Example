public class FifthWoodIslandHolder : IslandHolder
{
    private string _islandName = nameof(FifthWoodIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FifthWoodIsland : Island
{
    private const string _saveKey = nameof(FifthWoodIsland);
    public FifthWoodIsland() : base(_saveKey){}
}
