using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class CapturedTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _enemy.Captured += OnEnemyCaptured;
       
    }

    private void OnDisable()
    {
        _enemy.Captured -= OnEnemyCaptured;
    }

    private void OnEnemyCaptured()
    {
        NeedTransite = true;
    }
}
