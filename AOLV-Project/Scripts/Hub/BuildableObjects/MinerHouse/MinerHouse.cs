public class MinerHouse : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new MinerHouseStatus();
}

public class MinerHouseStatus : BuildingStatus
{
    private const string SaveKey = nameof(MinerHouseStatus);

    public MinerHouseStatus()
        : base(SaveKey)
    { }
}
