
public class AttackTransition : Transition
{
    private BotAttackStarter _attackStarter;

    private void Awake()
    {
        _attackStarter = GetComponentInChildren<BotAttackStarter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _attackStarter.TargetReached += OnTargetReached;
    }

    private void OnDisable()
    {
        _attackStarter.TargetReached -= OnTargetReached;
    }

    private void OnTargetReached()
    {
        NeedTransite = true;
    }
}
