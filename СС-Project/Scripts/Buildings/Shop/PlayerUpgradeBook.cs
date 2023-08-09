using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUpgradeBook", menuName = "GameAssets/PlayerUpgradeBook")]
public class PlayerUpgradeBook : ScriptableObject
{
    [SerializeField] private PlayerUpgradeGroup[] _upgrades;

    public PlayerUpgrade GetUpgrade(int upgradeStep, PlayerStatType type)
    {
        foreach (PlayerUpgradeGroup upgradeGroup in _upgrades)
        {
            if (upgradeGroup.Type == type)
            {
                if (upgradeGroup.Upgrades.Length > upgradeStep)
                    return upgradeGroup.Upgrades[upgradeStep];
                else
                    return null;
            }
        }

        return null;
    }
}

[Serializable]
public class PlayerUpgradeGroup
{
    [SerializeField] private PlayerStatType _type;
    [SerializeField] private PlayerUpgrade[] _upgrades;

    public PlayerStatType Type => _type;
    public PlayerUpgrade[] Upgrades => _upgrades;
}

[Serializable]
public class PlayerUpgrade
{
    [SerializeField] private int _cost;
    [SerializeField] private float _value;

    public int Cost => _cost;
    public float Value => _value;
}
