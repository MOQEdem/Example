using UnityEngine;

public class PatrolAfterPursuitTransition : Transition
{
    private BotVisibilityRange _visibilityRange;  

    private void Awake()
    {
        _visibilityRange = GetComponentInChildren<BotVisibilityRange>();       
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _visibilityRange.TargetLosted += OnTargetLosted;
        if (_visibilityRange.Target == null)
            OnTargetLosted();
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
