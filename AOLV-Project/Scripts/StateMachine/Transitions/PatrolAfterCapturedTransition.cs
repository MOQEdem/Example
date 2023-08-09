using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class PatrolAfterCapturedTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
       _enemy.Freed += OnEnemyFreed;
    }

    private void OnDisable()
    {
        _enemy.Freed -= OnEnemyFreed;
    }
    private void OnEnemyFreed(float health)
    {
        if (health > 0)
            NeedTransite = true;
    }
}
