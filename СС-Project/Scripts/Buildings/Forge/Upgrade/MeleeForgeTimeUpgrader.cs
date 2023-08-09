public class MeleeForgeTimeUpgrader : ForgeTimeUpgrader
{
    private const string SaveKey = nameof(MeleeForgeTimeUpgrader);

    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}
