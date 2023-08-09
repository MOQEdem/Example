using UnityEngine;

[RequireComponent(typeof(Bot))]
public class StunTransition : Transition
{
    [SerializeField] private StunState _stunState;

    private Bot _bot;

    private void Awake()
    {
        _bot = GetComponent<Bot>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _bot.Pushed += OnEnemyAttacked;
    }

    private void OnDisable()
    {
        _bot.Pushed -= OnEnemyAttacked;
    }

    private void OnEnemyAttacked(Vector3 position)
    {
        _stunState.SetAttackPoint(position);
        NeedTransite = true;
    }
}
