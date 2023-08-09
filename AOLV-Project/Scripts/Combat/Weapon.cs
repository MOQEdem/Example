using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon", order = 51)]
public class Weapon : ScriptableObject
{
    [SerializeField] private WeaponLevel _weaponLevel;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private GameObject _model;
    [SerializeField] private int _damage;

    public WeaponLevel Level => _weaponLevel;
    public WeaponType Type => _weaponType;
    public GameObject Model => _model;
    public int Damage => _damage;

}

public enum WeaponType
{
    Pick,
    Sword,
    Axe,
    Arrow
}

public enum WeaponLevel
{
    Base,
    Improved,
    Top
}
