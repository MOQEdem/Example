using UnityEngine;

public class RestTransition : Transition
{
    private MoveState _patrolState;

    private void Awake()
    {
        _patrolState = GetComponent<MoveState>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _patrolState.TargetReached += TryRest;
    }

    private void OnDisable()
    {
        _patrolState.TargetReached -= TryRest;
    }

    private void TryRest()
    {
        float minProbabilityValue = 0;
        float maxProbabilityValue = 10;
        float borderRestValue = 5;
        float randomValue = Random.Range(minProbabilityValue, maxProbabilityValue);
        NeedTransite = randomValue <= borderRestValue;
    }
}