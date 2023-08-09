public class RangedForgeResourceUpgrader : ForgeResourceUpgrader
{
    private const string SaveKey = nameof(RangedForgeResourceUpgrader);

    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}
