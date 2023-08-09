public class WeaponTypeSwitcher
{
    private WeaponType _curentWeaponType = WeaponType.Sword;
    private WeaponType _enemyPriorytyWeaponType;
    private UnitType _unitType;
    private ResourceType _resourceType;
    private WeaponTypeDictionaty[] _weaponTypeDictionaries =
    {
        new WeaponTypeDictionaty(WeaponType.Pick, UnitType.RockResourse, ResourceType.Stone),
        new WeaponTypeDictionaty(WeaponType.Axe, UnitType.WoodResource, ResourceType.Wood)
    };

    public WeaponTypeSwitcher(WeaponType weaponType, WeaponType enemyPriorytyWeaponType)
    {
        _curentWeaponType = weaponType;
        _enemyPriorytyWeaponType = enemyPriorytyWeaponType;
    }

    public bool TryGetNewWeaponType(Unit unit, out WeaponType weaponType)
    {
        weaponType = GetWeaponType(unit);
        if (_curentWeaponType != weaponType)
        {
            _curentWeaponType = weaponType;
            return true;
        }
        return false;
    }

    private WeaponType GetWeaponType(Unit unit)
    {
        if (unit.Type == UnitType.Enemy)
        {
            return _enemyPriorytyWeaponType;
        }
        if (TryGetResourceType(unit, out ResourceType resourceType))
        {
            foreach (var dictionaries in _weaponTypeDictionaries)
            {
                if (dictionaries.IsTargetTypes(unit.Type, resourceType, out WeaponType weaponType))
                {
                    return weaponType;
                }
            }
             throw new System.Exception("Not set _weaponTypeDictionaries value " + unit.Type + " " + resourceType);
        }
        throw new System.Exception("Not valid value");
    }

    public void SetWeaponType(WeaponType type)
    {
        _curentWeaponType = type;
    }

    private bool TryGetResourceType(Unit unit, out ResourceType resourceType)
    {
        resourceType = default;
        if (unit is ResourceSource resourceSource)
        {
            resourceType = resourceSource.ResourceType;
            return true;
        }
        return false;
    }

    private class WeaponTypeDictionaty
    {
        private WeaponType _weaponType;
        private UnitType _unitType;
        private ResourceType _resourceType;

        public WeaponTypeDictionaty(WeaponType weaponType, UnitType unitType, ResourceType resourceType)
        {
            _weaponType = weaponType;
            _unitType = unitType;
            _resourceType = resourceType;
        }

        public bool IsTargetTypes(UnitType unitType, ResourceType resourceType, out WeaponType weaponType)
        {
            if (unitType == _unitType && resourceType == _resourceType)
            {
                weaponType = _weaponType;
                return true;
            }
            weaponType = default;
            return false;
        }
    }
}
