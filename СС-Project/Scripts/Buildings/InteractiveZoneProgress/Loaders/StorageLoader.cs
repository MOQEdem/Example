public class StorageLoader : InteractiveZoneProgressLoader
{
    private const string SaveKey = nameof(StorageLoader);

    protected override InteractiveZoneProgress InitInteractiveZoneProgress() => new InteractiveZoneProgress(SaveKey);
}
