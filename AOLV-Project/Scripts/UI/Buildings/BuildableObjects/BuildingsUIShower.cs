using UnityEngine;
using UnityEngine.Serialization;

public class BuildingsUIShower : MonoBehaviour
{
    [SerializeField] private Resizer _resizer;
    [SerializeField] private Trigger<Player> _trigger;
    [SerializeField] private HubAd _adsButton;

    private Vector3 _normalSize;
    private Vector3 _smallSize = new Vector3(0.3f, 0.3f, 0.3f);

    private void Awake()
    {
        _normalSize = _resizer.transform.localScale;
        _resizer.transform.localScale = _smallSize;
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
    }

    private void OnPlayerEnter(Player player)
    {
        if (_adsButton != null)
            _adsButton.gameObject.SetActive(true);
        
        _resizer.Resize(1f, _normalSize);
    }

    private void OnPlayerExit(Player player)
    {
        _resizer.Resize(1f, _smallSize);
    }
}
