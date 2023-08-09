public class ThirdMeleeUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(ThirdMeleeUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
