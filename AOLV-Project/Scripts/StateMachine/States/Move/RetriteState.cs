using System;
using UnityEngine;

public class RetriteState : MoveState
{
    protected override Vector3 GetNextTarget()
    {
        Vector3 drakkarPosition = FindObjectOfType<DrakkarPosition>().transform.position;
        return drakkarPosition;
    }

    private void OnEnable()
    {
        ActivateMove();
        SetSpeed();
        SetTarget();
        MoveToTarget();
        StartMoveAnimation();
    }

    protected override void Move()
    {
        SetTarget();
        SetSwimpAnimationState();
        MoveToTarget();
        StartMoveAnimation();
    }
}
