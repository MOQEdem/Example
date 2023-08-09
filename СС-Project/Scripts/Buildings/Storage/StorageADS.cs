using Agava.YandexGames;
using UnityEngine;

public class StorageADS : MonoBehaviour
{
    private ADSShower _adsShower;
    private bool _isWatched = false;
    private bool _isWatching = false;

    public bool IsWatched => _isWatched;

    private void Awake()
    {
        _adsShower = new ADSShower();
    }

    private void OnEnable()
    {
        _adsShower.Rewarded += OnRewarded;
        _adsShower.Closed += OnClosed;
    }

    private void OnDisable()
    {
        _adsShower.Rewarded -= OnRewarded;
        _adsShower.Closed -= OnClosed;
    }

    private void Start()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    public void TryWatchADS()
    {

#if UNITY_EDITOR
        _isWatched = true;
        return;
#endif
        if (!_isWatching)
        {
            _isWatching = true;
            _adsShower.TryWatchADS();
        }
    }

    public void ResetWatchedStatus()
    {
        _isWatched = false;
    }

    private void OnRewarded()
    {
        _isWatched = true;
        _isWatching = false;
    }

    private void OnClosed()
    {
        _isWatched = false;
        _isWatching = false;
    }
}
