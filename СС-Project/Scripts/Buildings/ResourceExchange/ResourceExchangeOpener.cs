using UnityEngine;

public class ResourceExchangeOpener : MonoBehaviour
{
    [SerializeField] private UpgradeZone _squad;

    private BuildableObject _resourceExchanger;

    private void Awake()
    {
        _resourceExchanger = GetComponentInChildren<BuildableObject>();
    }

    private void OnEnable()
    {
        _squad.MaxLevelSet += OnMaxLevelSet;
    }

    private void OnDisable()
    {
        _squad.MaxLevelSet -= OnMaxLevelSet;
    }

    private void Start()
    {
        if (!_squad.IsFilled)
            _resourceExchanger.gameObject.SetActive(false);
    }

    private void OnMaxLevelSet()
    {
        _squad.MaxLevelSet -= OnMaxLevelSet;
        _resourceExchanger.gameObject.SetActive(true);
    }
}
