using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _swordAttackRadius = 1.5f;
    [SerializeField] private TargetDetector _targetDetecter;
    [SerializeField] private PlayerWeaponHitter _weaponHiter;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerWeaponContainer _currentWeapon;

    private float _damage = 2;


    private AttackState _attackState = AttackState.None;
    private Player _player;

    public PlayerWeaponHitter SwordHitter => _weaponHiter;

    public event UnityAction Attacked;

    // public event UnityAction AttackStarted;
    public event UnityAction AttackEneded;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _weaponHiter.HitEnded += OnHitEnded;
        _weaponHiter.TargetHited += OnTargetHited;
        _targetDetecter.Detected += OnTargetDetected;
    }

    private void OnDisable()
    {
        _weaponHiter.HitEnded -= OnHitEnded;
        _weaponHiter.TargetHited -= OnTargetHited;
        _targetDetecter.Detected -= OnTargetDetected;
    }

    public void UpgradeWeapon(WeaponUpgrade upgrade)
    {
        _weaponHiter.Upgrade(upgrade);
    }

    private void OnTargetDetected(Unit unit)
    {
        if (_attackState == AttackState.AutoAttack)
            return;
        StartAutoAttack(unit);
    }

    private void OnTargetHited()
    {
        Attacked?.Invoke();
    }

    private void OnHitEnded()
    {
        StopAttack();
        AttackEneded?.Invoke();
    }

    private void StartAutoAttack(Unit unit)
    {
        if (_currentWeapon.CurrentWeaponType == unit.PrioritiWeaponType)
        {
            _attackState = AttackState.AutoAttack;
            _playerMover.SetReducedSpeed();
            _weaponHiter.StartAutoAttack(_damage, unit);
        }
    }

    private void StopAttack()
    {
        _playerMover.SetDefaultSpeed();
        _attackState = AttackState.None;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _swordAttackRadius);
    }

    enum AttackState
    {
        None,
        AutoAttack,
    }
}