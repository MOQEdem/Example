public class ResourceExchange : Building
{
    private const string SaveKey = nameof(ResourceExchange);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
