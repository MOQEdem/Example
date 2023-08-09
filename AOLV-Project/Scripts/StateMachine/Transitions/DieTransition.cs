using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DieTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _enemy.Died += OnDie;
    }

    private void OnDisable()
    {
        _enemy.Died -= OnDie;
    }

    private void OnDie(Unit unit)
    {
        NeedTransite = true;
    }
}
