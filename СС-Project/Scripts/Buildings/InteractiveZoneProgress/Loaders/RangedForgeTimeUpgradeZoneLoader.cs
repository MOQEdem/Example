public class RangedForgeTimeUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(RangedForgeTimeUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
