public class FirstRangedUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(FirstRangedUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
