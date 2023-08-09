public class SecondCavalryUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(SecondCavalryUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


