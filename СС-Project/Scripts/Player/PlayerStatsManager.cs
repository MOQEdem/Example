using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private PlayableCharacter _playerCharacter;
    private PlayerMover _playerMover;
    private PlayerStack _playerStack;
    private PlayerUnitBuffer _playerUnitBuffer;
    private PlayerStats _playerStats;
    private PlayerMaterialSetter _playerMaterialSetter;

    private void Awake()
    {
        _playerCharacter = GetComponent<PlayableCharacter>();
        _playerMover = GetComponent<PlayerMover>();
        _playerStack = GetComponent<PlayerStack>();
        _playerUnitBuffer = GetComponentInChildren<PlayerUnitBuffer>();
        _playerMaterialSetter = GetComponent<PlayerMaterialSetter>();
    }

    private void Start()
    {
        _playerStats = new PlayerStats(_playerCharacter.CurrentHealthValue, _playerCharacter.CurrentDamageValue, _playerMover.Speed, _playerUnitBuffer.BuffRadius, _playerStack.Capacity, 0, 0);
        _playerStats.Load();


        UpgradeHealth(_playerStats.GetStatValue(PlayerStatType.Health), _playerStats.GetStatValue(PlayerStatType.ArmorGrade) - 1);
        UpgradeDamage(_playerStats.GetStatValue(PlayerStatType.Damage), _playerStats.GetStatValue(PlayerStatType.WeaponGrade) - 1);
        UpgradeSpeed(_playerStats.GetStatValue(PlayerStatType.Speed));
        UpgradeBuffRadius(_playerStats.GetStatValue(PlayerStatType.BuffRadius));
        UpgradeStackCapacity(_playerStats.GetStatValue(PlayerStatType.StackCapacity));
    }

    public void Save()
    {
        _playerStats.Save();
    }

    public void UpgradePlayerStat(float value, PlayerStatType type)
    {
        switch (type)
        {
            case PlayerStatType.Health:
                UpgradeHealth(value, _playerStats.GetStatValue(PlayerStatType.ArmorGrade));
                break;
            case PlayerStatType.Damage:
                UpgradeDamage(value, _playerStats.GetStatValue(PlayerStatType.WeaponGrade));
                break;
            case PlayerStatType.Speed:
                UpgradeSpeed(value);
                break;
            case PlayerStatType.BuffRadius:
                UpgradeBuffRadius(value);
                break;
            case PlayerStatType.StackCapacity:
                UpgradeStackCapacity(value);
                break;
            default:
                break;
        }
    }

    private void UpgradeHealth(float newHealth, float armorLevel)
    {
        _playerCharacter.UpgradeHealth((int)newHealth);
        _playerStats.SetStatValue(newHealth, PlayerStatType.Health);

        _playerStats.SetStatValue(armorLevel + 1, PlayerStatType.ArmorGrade);
        _playerMaterialSetter.SetArmorMaterial((int)armorLevel + 1);
    }

    private void UpgradeDamage(float newDamage, float weaponGrade)
    {
        _playerCharacter.UpgradeDamage((int)newDamage);
        _playerStats.SetStatValue(newDamage, PlayerStatType.Damage);

        _playerStats.SetStatValue(weaponGrade + 1, PlayerStatType.WeaponGrade);
        _playerMaterialSetter.SetSwordMaterial((int)weaponGrade + 1);
    }

    private void UpgradeSpeed(float newSpeed)
    {
        _playerMover.SetNewSpeed(newSpeed);
        _playerStats.SetStatValue(newSpeed, PlayerStatType.Speed);
    }

    private void UpgradeBuffRadius(float newRadius)
    {
        _playerUnitBuffer.SetNewBuffRadius(newRadius);
        _playerStats.SetStatValue(newRadius, PlayerStatType.BuffRadius);
    }

    private void UpgradeStackCapacity(float newCapacity)
    {
        _playerStack.UpgradeCapacity((int)newCapacity);
        _playerStats.SetStatValue(newCapacity, PlayerStatType.StackCapacity);
    }
}
