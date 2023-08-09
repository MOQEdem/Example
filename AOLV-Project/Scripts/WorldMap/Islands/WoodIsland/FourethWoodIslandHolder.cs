public class FourethWoodIslandHolder : IslandHolder
{
    private string _islandName = nameof(FouretWoodIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FouretWoodIsland : Island
{
    private const string _saveKey = nameof(FouretWoodIsland);
    public FouretWoodIsland() : base(_saveKey){}
}
