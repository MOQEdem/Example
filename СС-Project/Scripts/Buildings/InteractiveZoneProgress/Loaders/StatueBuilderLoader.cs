public class StatueBuilderLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(StatueBuilderLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
