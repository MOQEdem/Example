public class StableBuilding : Building
{
    private const string SaveKey = nameof(StableBuilding);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
