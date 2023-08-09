public class RangedForge : Building
{
    private const string SaveKey = nameof(RangedForge);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
