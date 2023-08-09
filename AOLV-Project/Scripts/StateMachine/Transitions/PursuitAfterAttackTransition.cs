using UnityEngine;

public class PursuitAfterAttackTransition : Transition
{
    [SerializeField] private AttackState _state;

    protected override void OnEnable()
    {
        base.OnEnable();
        _state.AttackEnded += OnAttackEnded;
    }

    private void OnDisable()
    {
        _state.AttackEnded -= OnAttackEnded;
    }

    private void OnAttackEnded()
    {
        NeedTransite = true;
    }
}
