public class FirstWoodIslandHolder : IslandHolder
{
    private string _islandName = nameof(FirstWoodIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FirstWoodIsland : Island
{
    private const string _saveKey = nameof(FirstWoodIsland);
    public FirstWoodIsland() : base(_saveKey){}
}

