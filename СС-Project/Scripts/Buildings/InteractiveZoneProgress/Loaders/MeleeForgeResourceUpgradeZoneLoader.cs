public class MeleeForgeResourceUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(MeleeForgeResourceUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
