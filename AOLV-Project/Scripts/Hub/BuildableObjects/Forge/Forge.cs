public class Forge : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new ForgeStatus();
}
