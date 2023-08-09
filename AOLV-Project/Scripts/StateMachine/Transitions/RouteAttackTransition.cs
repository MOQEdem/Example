using UnityEngine;

public class RouteAttackTransition : Transition
{
    [SerializeField] private BaseTimer _attackTimer;

    protected override void OnEnable()
    {
        base.OnEnable();
        _attackTimer.StartCounting();
        _attackTimer.TimeCounted += OnTimeCounted;
        if (_attackTimer.IsCounted == true)
        {
            NeedTransite = true;        
        }
    }

    private void OnDisable()
    {
        _attackTimer.TimeCounted -= OnTimeCounted;        
    }

    private void OnTimeCounted()
    {
        NeedTransite = true;
    }

}
