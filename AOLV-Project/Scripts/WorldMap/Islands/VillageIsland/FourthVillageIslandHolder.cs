public class FourthVillageIslandHolder : IslandHolder
{
    private string _islandName = nameof(FourthVillageIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FourthVillageIsland : Island
{
    private const string _saveKey = nameof(FourthVillageIsland);

    public FourthVillageIsland() : base(_saveKey){}
}