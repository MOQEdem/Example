public class HeavyBarracks : Building
{
    private const string SaveKey = nameof(HeavyBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
