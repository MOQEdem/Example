using UnityEngine;
public class PursuitTransition : Transition
{
    [SerializeField] private BotVisibilityRange _visibilityRange;

    protected override void OnEnable()
    {
        base.OnEnable();
        _visibilityRange.TargetDetected += OnTargetDetected;     
    }

    private void OnDisable()
    {       
        _visibilityRange.TargetDetected -= OnTargetDetected;      
    }   

    private void OnTargetDetected()
    {
        NeedTransite = true;
    }    
}