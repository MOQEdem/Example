public class DruidHouse : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new DruidHouseStatus();
}

public class DruidHouseStatus : BuildingStatus
{
    private const string SaveKey = nameof(DruidHouseStatus);

    public DruidHouseStatus()
        : base(SaveKey)
    { }
}
