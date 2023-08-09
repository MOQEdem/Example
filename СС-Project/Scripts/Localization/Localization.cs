using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using Agava.YandexGames;
using System.Collections;

public class Localization : MonoBehaviour
{
    [SerializeField] private RussianLanguageButton _russianLanguageButton;
    [SerializeField] private EnglishLanguageButton _englishLanguageButton;
    [SerializeField] private TurkeyLanguageButton _turkeyLanguageButton;

    private LeanLocalization _lean;
    private const string LocalizationLanguage = nameof(Localization);

    private IEnumerator Start()
    {
        _lean = GetComponent<LeanLocalization>();

        if (PlayerPrefs.HasKey(LocalizationLanguage))
        {
            SetLanguage(PlayerPrefs.GetString(LocalizationLanguage));
            yield return null;
        }
        else
        {
#if UNITY_EDITOR
            SetRussianLanguage();
#else
            yield return YandexGamesSdk.Initialize();

            string language = YandexGamesSdk.Environment.i18n.lang;

            switch (language)
            {
                case "ru":
                    SetLanguage(Language.Russian);
                    break;
                case "en":
                    SetLanguage(Language.English);
                    break;
                case "tr":
                    SetLanguage(Language.Turkish);
                    break;
            }
#endif
        }
    }

    private void OnEnable()
    {
        _russianLanguageButton.GetComponent<Button>().onClick.AddListener(SetRussianLanguage);
        _englishLanguageButton.GetComponent<Button>().onClick.AddListener(SetEnglishLanguage);
        _turkeyLanguageButton.GetComponent<Button>().onClick.AddListener(SetTurkishLanguage);
    }

    private void OnDisable()
    {
        _russianLanguageButton.GetComponent<Button>().onClick.RemoveListener(SetRussianLanguage);
        _englishLanguageButton.GetComponent<Button>().onClick.RemoveListener(SetEnglishLanguage);
        _turkeyLanguageButton.GetComponent<Button>().onClick.RemoveListener(SetTurkishLanguage);
    }

    private void SetLanguage(string language)
    {
        switch (language)
        {
            case Language.Russian:
                SetRussianLanguage();
                break;
            case Language.English:
                SetEnglishLanguage();
                break;
            case Language.Turkish:
                SetTurkishLanguage();
                break;
            default:
                SetEnglishLanguage();
                break;
        }
    }

    private void SetRussianLanguage()
    {
        _lean.SetCurrentLanguage(Language.Russian);
        PlayerPrefs.SetString(LocalizationLanguage, Language.Russian);
    }

    private void SetEnglishLanguage()
    {
        _lean.SetCurrentLanguage(Language.English);
        PlayerPrefs.SetString(LocalizationLanguage, Language.English);
    }

    private void SetTurkishLanguage()
    {
        _lean.SetCurrentLanguage(Language.Turkish);
        PlayerPrefs.SetString(LocalizationLanguage, Language.Turkish);
    }

}

public class Language
{
    public const string Russian = nameof(Russian);
    public const string English = nameof(English);
    public const string Turkish = nameof(Turkish);
}
