using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
//using Lean.Localization;
using UnityEngine;

public class SDKInitiolize : MonoBehaviour
{
    //private LeanLocalization _lean;



    private void Awake()
    {
#if YANDEX_GAMES
        YandexGamesSdk.CallbackLogging = true;
#endif
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        //string language = YandexGamesSdk.Environment.i18n.lang;
        //PlayerPrefs.SetString(Constants.LangKey, language);

        //InterstitialAd.Show();

        //_lean = GetComponent<LeanLocalization>();


        //switch (language)
        //{
        //    case "ru":
        //        _lean.SetCurrentLanguage("Russian");
        //        break;
        //    case "en":
        //        _lean.SetCurrentLanguage("English");
        //        break;
        //    case "tr":
        //        _lean.SetCurrentLanguage("Turkish");
        //        break;
        //}
    }
}
