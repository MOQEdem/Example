
public class NPC : Bot
{
    private void Start()
    {
        InitWeapon();  
    }

    protected override UnitType GetBotUnitType()
    {
        return UnitType.NPC;
    }
}
