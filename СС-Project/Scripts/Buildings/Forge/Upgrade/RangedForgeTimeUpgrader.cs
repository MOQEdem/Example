public class RangedForgeTimeUpgrader : ForgeTimeUpgrader
{
    private const string SaveKey = nameof(RangedForgeTimeUpgrader);

    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}
