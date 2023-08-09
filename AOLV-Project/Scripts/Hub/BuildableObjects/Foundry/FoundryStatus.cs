public class FoundryStatus : BuildingStatus
{
    private const string SaveKey = nameof(FoundryStatus);

    public FoundryStatus()
        : base(SaveKey)
    { }
}
