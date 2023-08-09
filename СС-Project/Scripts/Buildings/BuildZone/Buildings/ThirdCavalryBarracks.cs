public class ThirdCavalryBarracks : Building
{
    private const string SaveKey = nameof(ThirdCavalryBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
