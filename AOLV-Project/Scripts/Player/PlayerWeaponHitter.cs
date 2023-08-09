using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerWeaponHitter : MonoBehaviour
{
    [SerializeField] private PlayerWeaponContainer _weaponContainer;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _trail;
    [SerializeField] private ParticleSystem _fireTrail;
    [SerializeField] private ParticleSystem _electroTrail;

    private int _lastAutoAttackNumber = 0;    
    private float _damage;
    private WeaponType _weaponType = WeaponType.Sword;
    private WeaponTypeSwitcher _weaponSwitcher;
    public PlayerWeaponContainer WeaponContainer => _weaponContainer;

    public event UnityAction HitEnded;
    public event UnityAction TargetHited;

    private void Awake()
    {
        _weaponSwitcher = new WeaponTypeSwitcher(_weaponType, WeaponType.Sword);
    }

    private void OnEnable()
    {
        _weaponContainer.EnemyHited += OnEnemyHitted;
    }

    private void OnDisable()
    {
        _weaponContainer.EnemyHited -= OnEnemyHitted;
    }     

    public void StartAutoAttack(float damage, Unit unit)
    {        
        int minAttackNumber = 6;
        int maxAttackNumber = 8;
        int attackNumber = GetRandomAttackNumder(minAttackNumber, maxAttackNumber, _lastAutoAttackNumber);
        _lastAutoAttackNumber = attackNumber;
            //TryChangeWeapon(unit);
        Attack(attackNumber, damage);       
    }

    public void Upgrade(WeaponUpgrade weaponUpgrade)
    {
        _weaponType = weaponUpgrade.Weapon.Type;
        _weaponSwitcher.SetWeaponType(weaponUpgrade.Weapon.Type);
        _weaponContainer.SetNewWeapon(weaponUpgrade);
    }

    private void TryChangeWeapon( Unit unit)
    {
      if(_weaponSwitcher.TryGetNewWeaponType(unit, out WeaponType weaponType))
        {
            //_weaponType = weaponType;
           // _weaponContainer.ChangeWeapon(weaponType);
        }
    }    

    private void Attack(int attackNumber, float damage)
    {
        _damage = damage;
        _animator.SetInteger(AnimatorConst.AttackNumber, attackNumber);
        _animator.SetTrigger(AnimatorConst.Attack);
        _trail.Play();
    }

    public void StopAutoAttack()
    {
        _animator.StopPlayback();
        EndHit();
    }

    //Call from animator
    public void EndHit()
    {
        _weaponContainer.StopAttack();
        _trail.Stop();
        HitEnded?.Invoke();
    }

    //Call from animator
    public void ActivateSword()
    {
        _weaponContainer.StartAttack(_damage);
    }

    //Call from animator
    public void HitTarget()
    {
    }

    private void OnEnemyHitted()
    {
        TargetHited?.Invoke();
    }   

    private int GetRandomAttackNumder(int minAttackNumber, int maxAttackNumber, int lastAttackNumber)
    {
        int attackNumber = Random.Range(minAttackNumber, maxAttackNumber);
        if (attackNumber == lastAttackNumber)
        {
            if (attackNumber == minAttackNumber)
                attackNumber++;
            else
                attackNumber--;
        }
        return attackNumber;
    }
}
