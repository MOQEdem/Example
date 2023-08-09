public class Statue : Building
{
    private const string SaveKey = nameof(Statue);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
