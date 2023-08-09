public class FirstMeleeUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(FirstMeleeUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


