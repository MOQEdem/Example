public class FourthRockIslandHolder : IslandHolder
{
    private string _islandName = nameof(FourthRockIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class FourthRockIsland : Island
{
    private const string _saveKey = nameof(FourthRockIsland);
    public FourthRockIsland() : base(_saveKey){}
}