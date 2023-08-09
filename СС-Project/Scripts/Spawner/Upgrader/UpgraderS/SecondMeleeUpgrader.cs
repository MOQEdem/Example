public class SecondMeleeUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(SecondMeleeUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


