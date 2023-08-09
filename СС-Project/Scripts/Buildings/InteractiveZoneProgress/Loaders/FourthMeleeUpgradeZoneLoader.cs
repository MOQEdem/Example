public class FourthMeleeUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(FourthMeleeUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
