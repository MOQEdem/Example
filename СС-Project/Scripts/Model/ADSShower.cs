using System;
using UnityEngine;
using VideoAd = Agava.YandexGames.VideoAd;
#if CRAZY_GAMES
using CrazyGames;
#endif

public class ADSShower
{
    private float _normalTimeScale = 1f;

    public event Action Rewarded;
    public event Action Closed;

    public void TryWatchADS()
    {
        Time.timeScale = 0;

#if YANDEX_GAMES
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback: PlayScale, onErrorCallback: PlayScale);
#elif CRAZY_GAMES
        CrazyAds.Instance.beginAdBreakRewarded(Reward, PlayScale);
#elif DISTRIBUTION_GAMES
        GameDistributionSubscribeManager(true);
        GameDistribution.Instance.ShowRewardedAd();
#endif
    }

    private void GameDistributionSubscribeManager(bool isSubscribing)
    {
        if (isSubscribing)
        {
            GameDistribution.OnRewardGame += Reward;
            GameDistribution.OnResumeGame += PlayScale;
        }
        else
        {
            GameDistribution.OnRewardGame -= Reward;
            GameDistribution.OnResumeGame -= PlayScale;
        }
    }

    private void Reward()
    {
        Rewarded?.Invoke();
        Time.timeScale = _normalTimeScale;

#if DISTRIBUTION_GAMES                   
        GameDistributionSubscribeManager(false);
#endif
    }

    private void PlayScale(string strind)
    {
        Closed?.Invoke();
        Time.timeScale = _normalTimeScale;
    }

    private void PlayScale()
    {
        Closed?.Invoke();
        Time.timeScale = _normalTimeScale;

#if DISTRIBUTION_GAMES
        GameDistributionSubscribeManager(false);
#endif
    }
}
