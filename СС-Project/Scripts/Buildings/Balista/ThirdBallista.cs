public class ThirdBallista : Building
{
    private const string SaveKey = nameof(ThirdBallista);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
