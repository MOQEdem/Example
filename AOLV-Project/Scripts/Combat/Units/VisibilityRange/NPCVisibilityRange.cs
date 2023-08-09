
using UnityEngine;

public class NPCVisibilityRange : BotVisibilityRange
{
    [SerializeField] private UnitType _unitType;

    protected override UnitType GetPriorityTargetType(Unit target)
    {
        return _unitType;
    }

    protected override bool IsTargetUnit(Unit unit)
    {
        if (unit.Type == _unitType)
            return true;
        return false;
    }
}