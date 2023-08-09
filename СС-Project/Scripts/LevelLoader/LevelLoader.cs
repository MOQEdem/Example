using Agava.YandexMetrica;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private WinButton _winButtonObject;
    [SerializeField] private RestartButton _restartButtonObject;
    [SerializeField] private IntarstitialADSShower _adsShower;
    [SerializeField] private CloadSave _cloadSave;

    private Button _winButton;
    private Button _restartButton;
    private GameProgress _progress;
    private PlayerPrefManager _playerPrefManager = new PlayerPrefManager();

    public event Action LevelFinised;
    public event Action LevelRestarted;

    private void Awake()
    {
        _winButton = _winButtonObject.GetComponent<Button>();
        _restartButton = _restartButtonObject.GetComponent<Button>();

        _progress = new GameProgress();
        _progress.Load();

#if !UNITY_EDITOR
        YandexMetrica.Send($"level{_progress.CurrentLevel}Start");
#endif
    }

    private void OnEnable()
    {
        _winButton.onClick.AddListener(OnWinButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _winButton.onClick.RemoveListener(OnWinButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnWinButtonClick()
    {
        _adsShower.ShowInterstitial();

        StartCoroutine(WaitinfDelayBeforeLoad());
    }

    private void OnRestartButtonClick()
    {
        _adsShower.ShowInterstitial();

#if !UNITY_EDITOR
        YandexMetrica.Send("{levelRestarted");
#endif

        _playerPrefManager.ClearPrefsBetweenLevels();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator WaitinfDelayBeforeLoad()
    {
        PlayerPrefs.SetInt("ComplitedLevels", _progress.CurrentLevel);
        LevelFinised?.Invoke();

        yield return null;

#if !UNITY_EDITOR
        YandexMetrica.Send($"level{_progress.CurrentLevel}Complete");
#endif

        _progress.SetCurrentLevel(_progress.CurrentLevel + 1);

        var jsonString = JsonUtility.ToJson(_progress);

        _cloadSave.SetSavedData(SaveID.levelProgress, jsonString);

        _playerPrefManager.ClearPrefsBetweenLevels();

        SceneManager.LoadScene(_progress.GetCurrentLevelToLoad());
    }

    private IEnumerator WaitinfDelayBeforeRestart()
    {
        LevelRestarted?.Invoke();

        yield return null;

        _playerPrefManager.ClearPrefsBetweenLevels();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
