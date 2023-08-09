using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSquadUpgradeRequirements", menuName = "GameAssets/PlayerSquadUpgradeRequirements")]
public class PlayerSquadUpgradeRequirements : ScriptableObject
{
    [SerializeField] private PlayerSquadUpgrade[] _upgrades;

    public bool IsAbleToUpgrade(int gameLevel, int squadLevel, out int cost)
    {
        if (_upgrades.Length > squadLevel + 1 && _upgrades[squadLevel + 1].MinGameLevel <= gameLevel)
        {
            cost = _upgrades[squadLevel + 1].Cost;
            return true;
        }
        else
        {
            cost = 0;
            return false;
        }
    }

    public Dictionary<PlayerSquadZoneType, int> GetCapacity(int squadLevel)
    {
        Dictionary<PlayerSquadZoneType, int> capacity = new Dictionary<PlayerSquadZoneType, int>();
        int upgradeNumber;

        if (_upgrades.Length > squadLevel)
            upgradeNumber = squadLevel;
        else
            upgradeNumber = _upgrades.Length - 1;

        capacity.Add(PlayerSquadZoneType.Melee, _upgrades[upgradeNumber].NewMeleeCapacity);
        capacity.Add(PlayerSquadZoneType.Ranged, _upgrades[upgradeNumber].NewRangedCapacity);
        capacity.Add(PlayerSquadZoneType.Heavy, _upgrades[upgradeNumber].NewHeavyCapacity);
        capacity.Add(PlayerSquadZoneType.Cavalry, _upgrades[upgradeNumber].NewCavalryCapacity);

        return capacity;
    }
}

[Serializable]
public class PlayerSquadUpgrade
{
    [SerializeField] private int _minGameLevel;
    [SerializeField] private int _cost;
    [SerializeField] private int _newMeleeCapacity;
    [SerializeField] private int _newRangedCapacity;
    [SerializeField] private int _newCavalryCapacity;
    [SerializeField] private int _newHeavyCapacity;

    public int MinGameLevel => _minGameLevel;
    public int Cost => _cost;
    public int NewMeleeCapacity => _newMeleeCapacity;
    public int NewRangedCapacity => _newRangedCapacity;
    public int NewHeavyCapacity => _newHeavyCapacity;
    public int NewCavalryCapacity => _newCavalryCapacity;
}
