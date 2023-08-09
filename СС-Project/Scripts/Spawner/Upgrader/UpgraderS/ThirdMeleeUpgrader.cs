public class ThirdMeleeUpgrader : CharacterProduserUpgrader
{
    private const string SaveKey = nameof(ThirdMeleeUpgrader);
    protected override UpgraderStatus InitUpgraderStatus() => new UpgraderStatus(SaveKey);
}


