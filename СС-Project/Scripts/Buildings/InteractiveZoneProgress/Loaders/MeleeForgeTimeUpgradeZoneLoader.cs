public class MeleeForgeTimeUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(MeleeForgeTimeUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
