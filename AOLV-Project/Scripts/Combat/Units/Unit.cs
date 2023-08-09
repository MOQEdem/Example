using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ResourceBag))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private WeaponLevel _minWeaponLEvel;
    [SerializeField] private WeaponType[] _takeDamageFrom;
    [SerializeField] protected WeaponType _prioritiTakeDamage;

    private UnitType _type;
    private Health _health;
    private Armor _armor;
    private ResourceBag _resourceBag;
    private int _takenDamage;

    public Health Health => _health;
    public UnitType Type => _type;
    public ResourceBag ResourceBag => _resourceBag;
    public int TakenDamage => _takenDamage;
    public WeaponType PrioritiWeaponType => _prioritiTakeDamage;

    // public event UnityAction<Unit> HealthLost;
    public event Action<Unit> Died;
    public event Action<Vector3> Pushed;
    public event Action<AttackData> Hited;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _health.HealthLost += OnHealthLost;
    }

    private void OnDisable()
    {
        _health.HealthLost -= OnHealthLost;
    }

    public void TryTakeDamage(AttackData attackData)
    {
        if (_armor.IsDamaged(attackData.Weapon))
        {
            TakeDamage(attackData);
        }
    }

    public void Kill()
    {
        throw new NotImplementedException();
    }

    protected abstract void TakeDamage(AttackData attackData);

    protected abstract UnitType GetUnitType();

    protected abstract void Die();

    protected void Init()
    {
        _health = new Health(_maxHealth);
        _armor = new Armor(_minWeaponLEvel, _takeDamageFrom);
        _resourceBag = GetComponent<ResourceBag>();
        _type = GetUnitType();
    }

    protected void ReduceHealth(Weapon weapon)
    {
        if (weapon.Type == _prioritiTakeDamage)
        {
            _health.ApplyDamage(weapon.Damage);
            _takenDamage = weapon.Damage;
        }
        else
        {
            _health.ApplyDamage(weapon.Damage / 3);
            _takenDamage = weapon.Damage / 3;
        }
    }

    protected void Destroy()
    {
        Destroy(gameObject);
    }

    protected void CallEventPushed(Vector3 attackPosition)
    {
        Pushed?.Invoke(attackPosition);
    }

    protected void CallEventHited(AttackData attackData)
    {
        Hited?.Invoke(attackData);
    }

    protected void StartWaitingDestroy()
    {
        StartCoroutine(WaitDestroy());
    }

    private IEnumerator WaitDestroy()
    {
        yield return null;
        Destroy();
    }

    private void OnHealthLost()
    {
        Died?.Invoke(this);
        Die();
    }
}

public enum UnitType
{
    Player,
    Enemy,
    NPC,
    Resource,
    WoodResource,
    RockResourse
}
