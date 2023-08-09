public class Storage : Building
{
    private const string SaveKey = nameof(Storage);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
