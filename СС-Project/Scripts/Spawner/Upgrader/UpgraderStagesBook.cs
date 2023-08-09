using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "UpgraderStagesBook", menuName = "GameAssets/UpgraderStagesBook")]
public class UpgraderStagesBook : ScriptableObject
{
    [SerializeField] private UpgraderStageData[] _upgrades;

    public List<UpgraderStageData> GetListOfAvailableUpgrades()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        List<UpgraderStageData> availableUpgrades = new List<UpgraderStageData>();

        foreach (var upgrade in _upgrades)
        {
            if (upgrade.MinGameLevel <= currentLevel)
            {
                availableUpgrades.Add(upgrade);
            }
        }

        return availableUpgrades;
    }
}

[Serializable]
public class UpgraderStageData
{
    [SerializeField] private UpgraderLevel _level;
    [SerializeField] private int _cost;
    [SerializeField] private int _minGameLevel;
    [SerializeField] private int _minPlayerLevel;

    public UpgraderLevel Level => _level;
    public int Cost => _cost;
    public int MinGameLevel => _minGameLevel;
    public int MinPlayerLevel => _minPlayerLevel - 1;
}
