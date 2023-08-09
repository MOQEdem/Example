using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackState : State
{
    [SerializeField] private float _delayAfterAttack = 1f;

    private BotAttackStarter _attackStarter;
    private Coroutine _waitReloadAttack;
    protected Bot Bot { get; private set;}

    public event UnityAction AttackEnded;

    private void Awake()
    {
        _attackStarter = GetComponentInChildren<BotAttackStarter>();
        Bot = GetComponent<Bot>();
    }

    private void OnEnable()
    {
        AttackTarget(_attackStarter.Target);
    }

    protected abstract void AttackTarget(Unit target); 

    protected void StartReloadAttack()
    {
        if (_waitReloadAttack != null)
        {
            StopCoroutine(_waitReloadAttack);
        }
        StartCoroutine(WaitReloadAttack(_delayAfterAttack));
    }

    private IEnumerator WaitReloadAttack(float attackReloadTime)
    {
        yield return new WaitForSeconds(attackReloadTime);
        AttackEnded?.Invoke();
    }
}
