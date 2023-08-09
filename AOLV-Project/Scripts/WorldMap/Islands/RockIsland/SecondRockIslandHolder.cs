public class SecondRockIslandHolder : IslandHolder
{
    private string _islandName = nameof(SecondRockIsland);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class SecondRockIsland : Island
{
    private const string _saveKey = nameof(SecondRockIsland);
    public SecondRockIsland() : base(_saveKey){}
}