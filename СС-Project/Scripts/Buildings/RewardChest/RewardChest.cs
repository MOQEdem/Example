public class RewardChest : Building
{
    private const string SaveKey = nameof(RewardChest);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
