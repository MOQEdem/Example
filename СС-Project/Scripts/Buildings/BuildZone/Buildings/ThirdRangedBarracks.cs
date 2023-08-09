public class ThirdRangedBarracks : Building
{
    private const string SaveKey = nameof(ThirdRangedBarracks);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
