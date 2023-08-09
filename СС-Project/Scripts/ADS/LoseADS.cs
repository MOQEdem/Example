using UnityEngine;
using Agava.YandexGames;

public class LoseADS : MonoBehaviour
{
    [SerializeField] private PlayableCharacter _player;

    private ADSShower _adsShower;

    private void Awake()
    {
        _adsShower = new ADSShower();
    }

    private void OnEnable()
    {
        _adsShower.Rewarded += OnRewarded;
    }

    private void OnDisable()
    {
        _adsShower.Rewarded -= OnRewarded;
    }

    private void Start()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    public void onRewardButtonClick()
    {

#if UNITY_EDITOR
        _player.Resurrect();
        return;
#endif

        _adsShower.TryWatchADS();

    }

    private void OnRewarded()
    {
        _player.Resurrect();
    }
}
