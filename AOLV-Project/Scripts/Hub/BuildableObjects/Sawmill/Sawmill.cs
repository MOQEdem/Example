public class Sawmill : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new SawmillStatus();
}
