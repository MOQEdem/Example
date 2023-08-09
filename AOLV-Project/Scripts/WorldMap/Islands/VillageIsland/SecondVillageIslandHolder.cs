public class SecondVillageIslandHolder : IslandHolder
{
    private string _islandName = nameof(SecondVillageIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class SecondVillageIsland : Island
{
    private const string _saveKey = nameof(SecondVillageIsland);

    public SecondVillageIsland() : base(_saveKey){}
}
