public class DragonLairBuilding : Building
{
    private const string SaveKey = nameof(DragonLairBuilding);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
