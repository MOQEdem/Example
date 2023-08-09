using UnityEngine;

public class EscortTransition : Transition
{
    [SerializeField] private BotVisibilityRange _visibilityRange;

    protected override void OnEnable()
    {
        base.OnEnable();
        _visibilityRange.TargetLosted += OnTargetLosted;
        if (_visibilityRange.IsDetected == false)
            NeedTransite = true;
    }

    private void OnDisable()
    {
        _visibilityRange.TargetLosted -= OnTargetLosted;
    }

    private void OnTargetLosted()
    {
        NeedTransite = true;
    }
}
