public class MeleeForgeResourceUpgrader : ForgeResourceUpgrader
{
    private const string SaveKey = nameof(MeleeForgeResourceUpgrader);

    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}
