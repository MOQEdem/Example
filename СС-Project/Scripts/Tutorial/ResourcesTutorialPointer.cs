public class ResourcesTutorialPointer : TutorialPointer
{
    private PlayerTrigger _trigger;

    private void Awake()
    {
        _trigger = GetComponent<PlayerTrigger>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnStepDone;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnStepDone;
    }
}
