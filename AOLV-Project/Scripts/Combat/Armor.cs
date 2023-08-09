public class Armor
{
    private WeaponLevel _minWeaponLevel;
    private WeaponType[] _suitableEquipment;

    public Armor(WeaponLevel minWeaponLevel, WeaponType[] suitableEquipment)
    {
        _minWeaponLevel = minWeaponLevel;
        _suitableEquipment = suitableEquipment;
    }

    public bool IsDamaged(Weapon weapon)
    {
        foreach (WeaponType type in _suitableEquipment)
        {
            if (type == weapon.Type)
            {
                if (_minWeaponLevel <= weapon.Level)
                    return true;
            }
        }
        return false;
    }
}
