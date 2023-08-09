public class SecondWoodIslandHolder : IslandHolder
{
    private string _islandName = nameof(SecondWoodIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class SecondWoodIsland : Island
{
    private const string _saveKey = nameof(SecondWoodIsland);
    public SecondWoodIsland() : base(_saveKey){}
}

