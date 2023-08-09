public class SecondMeleeUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(SecondMeleeUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
