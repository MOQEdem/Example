public class SecondCavalryUpgradeZoneLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(SecondCavalryUpgradeZoneLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
