using System;
using UnityEngine;

public abstract class UnitEffectsStarter : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private ParticleSystem _damageParticle;
    [SerializeField] private PopUpText _popUp;
    protected Vector3 LastAttackPoint { get; private set; }
    protected Unit Unit => _unit;

    private void OnEnable()
    {
        _unit.Hited += OnUnitAttacked;
        _unit.Died += OnUnitDied;
    }

    private void OnDisable()
    {
        _unit.Hited -= OnUnitAttacked;
        _unit.Died -= OnUnitDied;
    }

    protected abstract void OnUnitDied(Unit unit);

    protected abstract void OnUnitAttacked(AttackData attackData);

    protected void PlayDamageParticles()
    {
        ParticleSystem damageParticle = Instantiate(_damageParticle, transform.position + transform.up, Quaternion.identity);
    }

    protected void SetLastAttackPoint(AttackData attackData)
    {
        LastAttackPoint = attackData.AttackPosition;
    }

    protected void ShowPopUpText()
    {
        PopUpText upText = Instantiate(_popUp, transform.position, Quaternion.identity);
        upText.SetPosition(transform.position);
        upText.SetText(Color.white, _unit.TakenDamage.ToString());
    }
}
