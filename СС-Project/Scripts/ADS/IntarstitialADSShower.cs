using Agava.YandexGames;
using UnityEngine;

public class IntarstitialADSShower : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _normalTimeScale = 1f;

    public void ShowInterstitial()
    {
        bool isFreeADS = _player.PlayerAccess.IsPlayerHaveAccess(AccessType.LevelFinish);

        if (!isFreeADS)
        {
#if YANDEX_GAMES
            InterstitialAd.Show(AdvScale, PlayScale, PlayScale);
#endif
        }
    }

    private void AdvScale()
    {
        Time.timeScale = 0;
    }

    private void PlayScale(bool boole)
    {
        Time.timeScale = _normalTimeScale;
    }

    private void PlayScale(string strind)
    {
        Time.timeScale = _normalTimeScale;
    }
}
