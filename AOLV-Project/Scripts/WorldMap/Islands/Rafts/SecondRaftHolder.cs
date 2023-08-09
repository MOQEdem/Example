public class SecondRaftHolder : IslandHolder
{
    private string _islandName = nameof(SecondRaft);

    protected override Island InitIsland()
    {
        return new Island(_islandName);
    } 
}

public class SecondRaft : Island
{
    private const string _saveKey = nameof(SecondRaft);

    public SecondRaft() : base(_saveKey){}
}
