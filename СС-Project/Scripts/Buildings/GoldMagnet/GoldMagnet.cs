public class GoldMagnet : Building
{
    private const string SaveKey = nameof(GoldMagnet);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
