
public class EnemyVisibilityRange : BotVisibilityRange
{
    private UnitType[] _unitTypes = new UnitType[] { UnitType.Player, UnitType.NPC };

    protected override UnitType GetPriorityTargetType(Unit target)
    {
        if (target.Type == UnitType.Player)
            return UnitType.Player;

        if (target.Type == UnitType.NPC)
            return UnitType.NPC;

        return UnitType.NPC;
    }

    protected override bool IsTargetUnit(Unit unit)
    {
        foreach (var unitType in _unitTypes)
        {
            if (unitType == unit.Type)
                return true;
        }
        return false;
    }
}
