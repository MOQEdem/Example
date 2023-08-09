public class SecondBallista : Building
{
    private const string SaveKey = nameof(SecondBallista);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
