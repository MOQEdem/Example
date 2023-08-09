public class LumbermanHouse : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new LumbermanHouseStatus();
}

public class LumbermanHouseStatus : BuildingStatus
{
    private const string SaveKey = nameof(LumbermanHouseStatus);

    public LumbermanHouseStatus()
        : base(SaveKey)
    { }
}
