public class Tavern : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new TavernStatus();
}
