using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

public class LocalizationSwitcher : MonoBehaviour
{
    private LeanLocalization _lean;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        string language = YandexGamesSdk.Environment.i18n.lang;

        _lean = GetComponent<LeanLocalization>();

        switch (language)
        {
            case "ru":
                _lean.SetCurrentLanguage("Russian");
                break;
            case "en":
                _lean.SetCurrentLanguage("English");
                break;
            case "tr":
                _lean.SetCurrentLanguage("Turkish");
                break;
        }
    }
}
