using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Agava.YandexMetrica;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadController : MonoBehaviour
{
    [SerializeField] private List<Pier> _loadTriggers;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Player _player;
    [SerializeField] private float _deathDelay = 2f;
    [SerializeField] private Button _resourceButton;
    [SerializeField] private Button _mapButton;
    [SerializeField] private CanvasGroup _resourceButtonGroup;
    [SerializeField] private CanvasGroup _mapButtonGroup;
    [SerializeField] private HubFoodHolder _food;

    private bool _isHubPier;
    private SceneSaver _sceneSaver;
    private string _previosSceneName;
    private Coroutine _waitingDelayAfterDeath;
    private float _normalScale;

    private void Start()
    {
        _normalScale = Time.timeScale;
        _sceneSaver = GetComponent<SceneSaver>();
        _previosSceneName = _sceneSaver.PreviosSceneName;
    }

    private void OnEnable()
    {
        DiactivateButtons();

        _player.Died += OnPlayerDied;

        _resourceButton.onClick.AddListener(StartLoadScene);
        _mapButton.onClick.AddListener(StartLoadMap);

        foreach (var trigger in _loadTriggers)
        {
            trigger.PlayerEnter += OnPlayerEnter;
            trigger.PlayerExit += OnPlayerExit;
        }
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;

        _resourceButton.onClick.RemoveListener(StartLoadScene);
        _mapButton.onClick.RemoveListener(StartLoadMap);

        foreach (var trigger in _loadTriggers)
        {
            trigger.PlayerEnter -= OnPlayerEnter;
            trigger.PlayerExit -= OnPlayerExit;
        }
    }

    public void AddLoadTrigger(Pier pier)
    {
        _loadTriggers.Add(pier);

        pier.PlayerEnter += OnPlayerEnter;
        pier.PlayerExit += OnPlayerExit;
    }

    private void OnPlayerEnter(bool isHubPier)
    {
        _isHubPier = isHubPier;

        ActivateButtons();
    }

    private void OnPlayerExit(bool isHubPier)
    {
        DiactivateButtons();
    }

    private void OnPlayerDied(Unit unit)
    {
        if (_waitingDelayAfterDeath == null)
            _waitingDelayAfterDeath = StartCoroutine(WaitingDelayAfterDeath());
    }

    private void ActivateButtons()
    {
        _resourceButtonGroup.interactable = true;
        _resourceButtonGroup.blocksRaycasts = true;
        _resourceButtonGroup.alpha = 1f;
        if (_previosSceneName == SceneName.WorldMap || SceneManager.GetActiveScene().name == SceneName.NewHub)
        {
            if (_food.Value >= 50)
            {
                _mapButtonGroup.interactable = true;
                _mapButtonGroup.blocksRaycasts = true;
                _mapButtonGroup.alpha = 1f;
            }
            else
                _mapButtonGroup.alpha = 1f;
        }
    }

    private void DiactivateButtons()
    {
        _mapButtonGroup.interactable = false;
        _mapButtonGroup.blocksRaycasts = false;
        _mapButtonGroup.alpha = 0f;
        _resourceButtonGroup.interactable = false;
        _resourceButtonGroup.blocksRaycasts = false;
        _resourceButtonGroup.alpha = 0f;
    }

    private void StartLoadScene()
    {
        _sceneLoader.LoadScene(_isHubPier);
    }

    private void StartLoadMap()
    {
        if (_food.Value > 0)
            _sceneLoader.LoadMap();
    }

    private IEnumerator WaitingDelayAfterDeath()
    {
        YandexMetrica.Send("fail");
        yield return _player.Timer.CountingDown(_deathDelay);

        _sceneLoader.LoadScene(false);
    }

    public void OnbuttonClick()
    {
        Time.timeScale = 0;
        Agava.YandexGames.VideoAd.Show(onRewardedCallback: Respawn, onCloseCallback: PlayScale,
            onErrorCallback: PlayScale);
    }

    private void Respawn()
    {
        YandexMetrica.Send("AdWatched");
        _player.Timer.TimerBrain.Stop();
        StopCoroutine(_waitingDelayAfterDeath);
        _waitingDelayAfterDeath = null;
    }

    private void PlayScale()
    {
        Time.timeScale = _normalScale;
    }

    private void PlayScale(string strin)
    {
        Time.timeScale = _normalScale;
    }
}