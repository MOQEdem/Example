using UnityEngine;
#if CRAZY_GAMES
using CrazyGames;
#endif

public class WinButtonADS : MonoBehaviour
{
    private float _normalTimeScale = 1f;

    public void ShowInterstitial()
    {
#if CRAZY_GAMES
        CrazyAds.Instance.beginAdBreak(PlayScale, PlayScale);
#endif
    }

    private void PlayScale() =>
        Time.timeScale = _normalTimeScale;
}
