using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _timeToDestroy = 5f;
    [SerializeField] private float _offsetYPosition = 2f;
    [SerializeField] private ParticleSystem _destroyEffect;

    private Weapon _weapon;
    private UnitType[] _unitTypes;
    protected Unit _unit;

    private Vector3 _direction;
    private Vector3 _target;

    public void Shoot(Vector3 target, Weapon weapon, Unit unit)
    {
        _target = target;
        _target = new Vector3(target.x, target.y + _offsetYPosition, target.z);
        _unit = unit;
        _direction = (_target - transform.position).normalized;
        Quaternion targetQuternion = Quaternion.LookRotation(_direction, Vector3.up);
        transform.rotation = targetQuternion;
        _weapon = weapon;
        Destroy(gameObject, _timeToDestroy);
    }

    public void SetTypesTargets(UnitType[] unitTypes)
    {
        _unitTypes = unitTypes;
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if(unit.Type == UnitType.Resource)
            {
                Destroy();
                return;
            }
            if (IsTargetUnit(unit))
            {
                unit.TryTakeDamage(new AttackData(_weapon, transform.position, unit));
                Destroy();
            }
        }      
    }    

    private bool IsTargetUnit(Unit unit)
    {
        foreach (var unitType in _unitTypes)
        {
            if (unitType == unit.Type)
                return true;
        }
        return false;
    }
    private void Destroy()
    {
        if (_destroyEffect != null)
        {
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
