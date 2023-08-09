public class FirstBallista : Building
{
    private const string SaveKey = nameof(FirstBallista);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}

