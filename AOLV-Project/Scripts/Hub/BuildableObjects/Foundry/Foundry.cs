public class Foundry : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new FoundryStatus();
}
