public class ThirdCavalryUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(ThirdCavalryUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


