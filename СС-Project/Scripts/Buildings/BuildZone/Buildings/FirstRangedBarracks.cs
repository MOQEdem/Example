public class FirstRangedBarracks : Building
{
    private const string SaveKey = nameof(FirstRangedBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
