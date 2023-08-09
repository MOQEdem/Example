public class SecondRangedBarracks : Building
{
    private const string SaveKey = nameof(SecondRangedBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
