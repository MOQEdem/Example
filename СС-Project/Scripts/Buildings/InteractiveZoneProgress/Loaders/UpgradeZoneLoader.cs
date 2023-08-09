public class UpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(UpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
