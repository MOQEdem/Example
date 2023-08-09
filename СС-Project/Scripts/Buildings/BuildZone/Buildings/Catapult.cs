public class Catapult : Building
{
    private const string SaveKey = nameof(Catapult);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
