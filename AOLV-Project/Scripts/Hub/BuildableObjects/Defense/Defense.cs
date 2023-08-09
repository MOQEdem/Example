public class Defense : BuildableObject
{
    protected override BuildingStatus InitBuildingStatus() => new DefenseStatus();
}
