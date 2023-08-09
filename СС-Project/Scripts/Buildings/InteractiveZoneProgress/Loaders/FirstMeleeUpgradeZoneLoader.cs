public class FirstMeleeUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(FirstMeleeUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
