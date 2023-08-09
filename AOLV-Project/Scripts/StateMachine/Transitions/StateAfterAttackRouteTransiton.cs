using System;
using UnityEngine;

public class StateAfterAttackRouteTransiton : Transition
{
    [SerializeField] private AttacksRouter _attacksRouter;

    protected override void OnEnable()
    {
        base.OnEnable();
        _attacksRouter.Routed += OnRouted;
        if(_attacksRouter.NextState != null)
        {
            OnRouted(_attacksRouter.NextState);
        }
    }

    private void OnDisable()
    {
        _attacksRouter.Routed -= OnRouted;
    }

    private void OnRouted(State state)
    {
        SetState(state);
        NeedTransite = true;
    }   
}
