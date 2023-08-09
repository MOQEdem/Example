public class FirstVillageIslandHolder : IslandHolder
{
    private string _islandName = nameof(FirstVillageIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FirstVillageIsland : Island
{
    private const string _saveKey = nameof(FirstVillageIsland);

    public FirstVillageIsland() : base(_saveKey){}
}
