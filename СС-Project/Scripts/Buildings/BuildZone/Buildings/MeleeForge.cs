public class MeleeForge : Building
{
    private const string SaveKey = nameof(MeleeForge);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
