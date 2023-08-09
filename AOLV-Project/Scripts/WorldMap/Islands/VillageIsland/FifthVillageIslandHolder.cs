public class FifthVillageIslandHolder : IslandHolder
{
    private string _islandName = nameof(FifthVillageIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FifthVillageIsland : Island
{
    private const string _saveKey = nameof(FifthVillageIsland);

    public FifthVillageIsland() : base(_saveKey){}
}