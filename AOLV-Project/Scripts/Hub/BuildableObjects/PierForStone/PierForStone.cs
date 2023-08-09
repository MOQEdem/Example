public class PierForStone : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new PierForStoneStatus();
}
