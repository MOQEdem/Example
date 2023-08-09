public class ThirdMeleeBarracks : Building
{
    private const string SaveKey = nameof(ThirdMeleeBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
