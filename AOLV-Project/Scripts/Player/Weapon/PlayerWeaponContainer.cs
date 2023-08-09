using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class PlayerWeaponContainer : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private GameObject _weaponModel;
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private Unit _unit;

    private float _damage;
    private bool _isActive = false;
    private HashSet<Unit> _hittedUnits;
    private int _weaponIndex;

    public WeaponType CurrentWeaponType => _currentWeapon.Type;
    
    public event UnityAction EnemyHited;

    private void Awake()
    {
        ChangeWeapon(_currentWeapon.Type);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("1"))
            ChangeWeapon(WeaponType.Sword);
        if (Input.GetKey("2"))
            ChangeWeapon(WeaponType.Axe);
        if (Input.GetKey("3"))
            ChangeWeapon(WeaponType.Pick);
    }

    public void OnAxeClics() => ChangeWeapon(WeaponType.Axe);
    public void OnPickClics() => ChangeWeapon(WeaponType.Pick);
    public void OnSwordClics() => ChangeWeapon(WeaponType.Sword);

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive == false)
            return;
        if (other.gameObject.TryGetComponent(out Unit unit))
        {
            if (unit.Type == UnitType.NPC)
                return;
            if (_hittedUnits.Contains(unit))
                return;
            unit.TryTakeDamage(new AttackData(_currentWeapon, _attackPoint.position, _unit));
            EnemyHited?.Invoke();
            _hittedUnits.Add(unit);
        }
    }

    public void StartAttack(float damage)
    {
        _hittedUnits = new HashSet<Unit>();
        _damage = damage;
        _isActive = true;
        _triggerCollider.enabled = true;
    }

    public void SetNewWeapon(WeaponUpgrade upgrade)
    {

        for (int i = 0; i < _weapons.Count; i++)
        {
            if (_weapons[i].Type == upgrade.Weapon.Type)
            {
                _weapons[i] = upgrade.Weapon;
                ChangeWeapon(_weapons[i].Type);
                return;
            }
        }
        throw new Exception("Not set WeaponTypes " + upgrade.Weapon.Type);
    }

    public void StopAttack()
    {
        _isActive = false;
        _triggerCollider.enabled = false;
        _hittedUnits.Clear();
    }

    public void ChangeWeapon(WeaponType weaponType)
    {     
        Destroy(_weaponModel);
        _currentWeapon = _weapons.First(weapon => weapon.Type == weaponType);
        _weaponModel = Instantiate(_currentWeapon.Model, transform);
    }
}
