public class SecondRangedUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(SecondRangedUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


