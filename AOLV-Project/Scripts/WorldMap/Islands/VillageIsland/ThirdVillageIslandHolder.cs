public class ThirdVillageIslandHolder : IslandHolder
{
    private string _islandName = nameof(ThirdVillageIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class ThirdVillageIsland : Island
{
    private const string _saveKey = nameof(ThirdVillageIsland);

    public ThirdVillageIsland() : base(_saveKey){}
}
