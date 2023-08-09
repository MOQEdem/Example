public class FourthMeleeBarracks : Building
{
    private const string SaveKey = nameof(FourthMeleeBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
