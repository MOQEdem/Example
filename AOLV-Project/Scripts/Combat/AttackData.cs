using UnityEngine;

public struct AttackData
{
    public readonly Weapon Weapon;
    public readonly Vector3 AttackPosition;
    public readonly Unit Unit;

    public AttackData(Weapon weapon, Vector3 attackPosition, Unit unit)
    {
        Weapon = weapon;
        AttackPosition = attackPosition;
        Unit = unit;
    }
}