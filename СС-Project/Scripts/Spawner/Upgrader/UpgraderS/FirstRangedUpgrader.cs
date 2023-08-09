public class FirstRangedUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(FirstRangedUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


