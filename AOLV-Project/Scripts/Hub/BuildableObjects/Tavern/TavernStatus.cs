public class TavernStatus : BuildingStatus
{
    private const string SaveKey = nameof(TavernStatus);

    public TavernStatus()
        : base(SaveKey)
    { }
}
