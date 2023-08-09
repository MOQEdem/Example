public class BattleTutorialPointer : TutorialPointer
{
    private BattleStarter _battleStarter;

    private void Awake()
    {
        _battleStarter = GetComponent<BattleStarter>();
    }

    private void OnEnable()
    {
        _battleStarter.BattleStarted += OnStepDone;
    }

    private void OnDisable()
    {
        _battleStarter.BattleStarted -= OnStepDone;
    }
}
