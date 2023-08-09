using UnityEngine;

public class PursuitState : MoveState
{
    [SerializeField] private ResourceBag _resourceBag;
    
    private Unit _targetUnit => _visibilityRange.Target;
    private BotVisibilityRange _visibilityRange;
    private bool _bagIsFull = false;
    
    public Unit Target => _targetUnit;

    private void Awake()
    {
        _visibilityRange = GetComponentInChildren<BotVisibilityRange>();
    }

    private void OnEnable()
    {
        ActivateMove();
        SetSpeed();
        SetTarget();
        MoveToTarget();
        StartMoveAnimation();
    }
    
    protected override Vector3 GetNextTarget()
    {
        if (_targetUnit == null)
            return transform.position;
        return _targetUnit.transform.position;
    }

    protected override void Move()
    {
        
        SetTarget();
        MoveToTarget();
        SetSwimpAnimationState();
    }

    private void HolderFilled()
    {
        _bagIsFull = true;
    }
}
