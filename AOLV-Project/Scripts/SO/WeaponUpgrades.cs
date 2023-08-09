using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrades", menuName = "GameAssets/WeaponUpgrades")]
public class WeaponUpgrades : ScriptableObject
{
    [SerializeField] private WeaponUpgrade[] _upgradesBook;

    public WeaponUpgrade[] UpgradesBook => _upgradesBook;
    public int UpgradesCount => _upgradesBook.Length;
}

[Serializable]
public class WeaponUpgrade
{
    [SerializeField] private int _cost;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private Weapon _weapon;

    public int Cost => _cost;
    public ResourceType ResourceType => _resourceType;
    public Weapon Weapon => _weapon;
}
