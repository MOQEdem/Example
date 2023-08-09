public class SecondCavalryBarracks : Building
{
    private const string SaveKey = nameof(SecondCavalryBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
