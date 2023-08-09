public class FirstCavalryUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(FirstCavalryUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
