public class SecondMeleeBarracks : Building
{
    private const string SaveKey = nameof(SecondMeleeBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
