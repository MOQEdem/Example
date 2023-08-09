public class FirstMeleeBarracks : Building
{
    private const string SaveKey = nameof(FirstMeleeBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
