public class ThirdRangedUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(ThirdRangedUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
