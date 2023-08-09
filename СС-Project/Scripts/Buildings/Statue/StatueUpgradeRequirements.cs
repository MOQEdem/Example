using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatueUpgradeRequirements", menuName = "GameAssets/StatueUpgradeRequirements")]
public class StatueUpgradeRequirements : ScriptableObject
{
    [SerializeField] private StatueUpgrade[] _upgrades;

    public bool IsAbleToUpgrade(int currentLevel, out int cost)
    {
        if (_upgrades.Length > currentLevel)
        {
            cost = _upgrades[currentLevel].Cost;
            return true;
        }
        else
        {
            cost = 0;
            return false;
        }
    }
}

[Serializable]
public class StatueUpgrade
{
    [SerializeField] private int _cost;

    public int Cost => _cost;
}
