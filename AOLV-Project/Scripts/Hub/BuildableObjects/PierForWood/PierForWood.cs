public class PierForWood : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new PierForWoodStatus();
}
