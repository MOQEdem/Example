using System;
using UnityEngine;

[Serializable]
public class PlayerStats : SavedObject<PlayerStats>
{
    private const string SaveKey = nameof(PlayerStats);

    [SerializeField] private PlayerStat _health;
    [SerializeField] private PlayerStat _damage;
    [SerializeField] private PlayerStat _speed;
    [SerializeField] private PlayerStat _buffRadius;
    [SerializeField] private PlayerStat _stackCapacity;
    [SerializeField] private PlayerStat _armorGrade;
    [SerializeField] private PlayerStat _weaponGrade;

    [SerializeField] private PlayerStat[] _stats;

    public PlayerStats(int health, int damage, float speed, float buffRadius, int stackCapacity, int armorGrade, int weaponGrade) : base(SaveKey)
    {
        _health = new PlayerStat(health, PlayerStatType.Health);
        _damage = new PlayerStat(damage, PlayerStatType.Damage);
        _speed = new PlayerStat(speed, PlayerStatType.Speed);
        _buffRadius = new PlayerStat(buffRadius, PlayerStatType.BuffRadius);
        _stackCapacity = new PlayerStat(stackCapacity, PlayerStatType.StackCapacity);
        _armorGrade = new PlayerStat(armorGrade, PlayerStatType.ArmorGrade);
        _weaponGrade = new PlayerStat(weaponGrade, PlayerStatType.WeaponGrade);

        _stats = new PlayerStat[] { _health, _damage, _speed, _buffRadius, _stackCapacity, _armorGrade, _weaponGrade };
    }

    public void SetStatValue(float value, PlayerStatType type)
    {
        foreach (var stat in _stats)
        {
            if (stat.Type == type)
                stat.SetValue(value);
        }
    }

    public float GetStatValue(PlayerStatType type)
    {
        foreach (var stat in _stats)
        {
            if (stat.Type == type)
                return stat.Value;
        }

        return 0;
    }

    protected override void OnLoad(PlayerStats loadedObject)
    {
        _health = loadedObject._health;
        _damage = loadedObject._damage;
        _speed = loadedObject._speed;
        _buffRadius = loadedObject._buffRadius;
        _stackCapacity = loadedObject._stackCapacity;
        _armorGrade = loadedObject._armorGrade;
        _weaponGrade = loadedObject._weaponGrade;

        _stats = loadedObject._stats;
    }

    [Serializable]
    public class PlayerStat
    {
        [SerializeField] private float _value;
        [SerializeField] private PlayerStatType _type;

        public float Value => _value;
        public PlayerStatType Type => _type;

        public PlayerStat(float value, PlayerStatType type)
        {
            _value = value;
            _type = type;
        }

        public void SetValue(float value)
        {
            _value = value;
        }
    }
}

[Serializable]
public enum PlayerStatType
{
    Health,
    Damage,
    Speed,
    StackCapacity,
    BuffRadius,
    ArmorGrade,
    WeaponGrade
}
