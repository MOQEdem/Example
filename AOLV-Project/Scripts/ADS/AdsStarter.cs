using System.Collections;
using System.Collections.Generic;
using Agava.VKGames;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsStarter : MonoBehaviour
{
    private IEnumerator Start()
    {
#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize();
        InterstitialAd.Show(OnADOpen, OnADClose, OnADError, OnADOffline);

        if (PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
            PlayerAccount.RequestPersonalProfileDataPermission();

#endif
#if VK_GAMES
         yield return VKGamesSdk.Initialize();
         InterstitialAd.Show();
#endif
        yield return null;
    }

    private void OnADOpen()
    {
        Time.timeScale = 0;
    }

    private void OnADClose(bool booleon)
    {
        Time.timeScale = 1;
    }

    private void OnADError(string text)
    {
        Time.timeScale = 1;
    }

    private void OnADOffline()
    {
        Time.timeScale = 1;
    }
}
