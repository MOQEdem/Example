public class ProdusserTutorialPointer : TutorialPointer
{
    private BuildZone _buildZone;

    private void Awake()
    {
        _buildZone = GetComponent<BuildZone>();
    }

    private void OnEnable()
    {
        _buildZone.Build += OnStepDone;
    }

    private void OnDisable()
    {
        _buildZone.Build -= OnStepDone;
    }
}
