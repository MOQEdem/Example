using System;
using System.Collections;
using System.Collections.Generic;
using Agava.VKGames;
using Agava.YandexGames;
using Agava.YandexMetrica;
using UnityEngine;
using VideoAd = Agava.YandexGames.VideoAd;

public class EmptyFood : MonoBehaviour
{
    [SerializeField] private int _Reward;
    [SerializeField] private DrakkarMover _mover;
    [SerializeField] private float _resizeTime;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private HubFoodHolder _food;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _duration;
    
    private Resizer _resizer;
    private Vector3 _normalScale;
    private float _normalTimeScale;
    
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _normalTimeScale = Time.timeScale;
    }

    private void Start()
    {
        _resizer = GetComponent<Resizer>();
        _normalScale = transform.localScale;
        _resizer.Resize(0.1f, 0f);
    }

    private void OnEnable()
    {
        _mover.FoodIsEmpty += OnFoodIsEmpty;
    }

    private void OnDisable()
    {
        _mover.FoodIsEmpty -= OnFoodIsEmpty;
    }

    private void OnFoodIsEmpty()
    {
        _resizer.Resize(_resizeTime, _normalScale);
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        yield return _timer.CountingDown(_duration);
    }
    
    public void OnHubButtonClick()
    {
        _sceneLoader.LoadScene(SceneName.NewHub);
    }

    public void OnWatchVideoClick()
    {
        Time.timeScale = 0;
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback: PlayScale);
    }

    private void Reward()
    {
        YandexMetrica.Send("AdWatched");
        _food.Add(_Reward);
        _resizer.Resize(0.1f, 0f);
    }
    
    private void PlayScale()
    {
        Time.timeScale = _normalTimeScale;
    }
}