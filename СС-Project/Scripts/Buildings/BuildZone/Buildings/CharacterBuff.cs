public class CharacterBuff : Building
{
    private const string SaveKey = nameof(CharacterBuff);

    protected override BuildingStatus InitBuildingStatus() => new BuildingStatus(SaveKey);
}
