public class SecondRangedUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(SecondRangedUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
