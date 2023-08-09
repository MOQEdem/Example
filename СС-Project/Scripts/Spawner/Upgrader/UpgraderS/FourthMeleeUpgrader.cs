public class FourthMeleeUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(FourthMeleeUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}
