public class FirstCavalryUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(FirstCavalryUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


