public class FirstCavalryBarracks : Building
{
    private const string SaveKey = nameof(FirstCavalryBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
