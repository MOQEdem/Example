public class ThirdRangedUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(ThirdRangedUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


