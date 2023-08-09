public class RangedForgeResourceUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(RangedForgeResourceUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
