public class BarnBuilding : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new BarnStatus();
}

public class BarnStatus : BuildingStatus
{
    private const string SaveKey = nameof(BarnStatus);

    public BarnStatus()
        : base(SaveKey)
    { }
}
