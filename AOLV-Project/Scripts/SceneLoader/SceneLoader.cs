using System.Collections;
using Agava.YandexMetrica;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingPercentage;
    [SerializeField] private Image _loadingProgressBar;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerStoneHolder _playerStoneHolder;
    [SerializeField] private PlayerWoodHolder _playerWoodHolder;

    private AsyncOperation _loadingSceneOperation;
    private Coroutine _loadingScene;

    private IslandCounter _islandCounter;

    private void Start()
    {
        _islandCounter = new IslandCounter();
        _islandCounter.Load();

        _animator.SetTrigger(AnimatorLoadScreen.Trigger.EndTransition);
        _loadingProgressBar.fillAmount = 1;
        _loadingPercentage.text = "100";
    }

    public void LoadScene(bool isHubPier)
    {
        if (_loadingScene == null)
            _loadingScene = StartCoroutine(LoadingScene(GetSceneName(isHubPier)));
    }

    public void LoadScene(string sceneName)
    {
        if (_loadingScene == null)
        {
            if (sceneName != SceneName.NewHub)
            {
                YandexMetrica.Send("startNewIsland");
                YandexMetrica.Send($"level{_islandCounter.IslandNumber}Start");
            }
            _loadingScene = StartCoroutine(LoadingScene(sceneName));
        }
    }

    public void LoadMap()
    {
        if (_loadingScene == null)
        {
            if (SceneManager.GetActiveScene().name != SceneName.NewHub)
            {
                YandexMetrica.Send($"level{_islandCounter.IslandNumber}Complete");
                _islandCounter.AddIsland();
            }

            _loadingScene = StartCoroutine(LoadingScene(SceneName.WorldMap));
        }
    }

    private string GetSceneName(bool isHubPier)
    {
        if (isHubPier)
        {
            if (_playerWoodHolder.Value >= _playerStoneHolder.Value)
            {
                YandexMetrica.Send("startNewIsland");
                YandexMetrica.Send($"level{_islandCounter.IslandNumber}Start");

                return SceneName.StoneIsland;
            }
            else
            {
                YandexMetrica.Send("startNewIsland");
                YandexMetrica.Send($"level{_islandCounter.IslandNumber}Start");

                return SceneName.WoodIsland;
            }
        }
        else
        {
            YandexMetrica.Send($"level{_islandCounter.IslandNumber}Complete");
            YandexMetrica.Send("IslandCompleted");


            _islandCounter.AddIsland();

            return SceneName.NewHub;
        }
    }

    private IEnumerator LoadingScene(string sceneName)
    {
        _animator.SetTrigger(AnimatorLoadScreen.Trigger.StartTransition);

        _loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadingSceneOperation.allowSceneActivation = false;

        while (!_loadingSceneOperation.isDone)
        {
            _loadingPercentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";

            _loadingProgressBar.fillAmount = Mathf.Lerp(_loadingProgressBar.fillAmount, _loadingSceneOperation.progress, Time.deltaTime * 5);

            yield return null;
        }
    }

    public void OnAnimationOver()
    {
        _loadingSceneOperation.allowSceneActivation = true;
    }

    public static class AnimatorLoadScreen
    {
        public static class Trigger
        {
            public const string StartTransition = nameof(StartTransition);
            public const string EndTransition = nameof(EndTransition);
        }
    }
}

public static class SceneName
{
    public const string Screensaver = nameof(Screensaver);
    public const string Tutorial = nameof(Tutorial);
    public const string CurvedTest = nameof(CurvedTest);
    public const string NewHub = nameof(NewHub);
    public const string WorldMap = nameof(WorldMap);
    public const string WoodIsland = nameof(WoodIsland);
    public const string StoneIsland = nameof(StoneIsland);
    public const string Village = nameof(Village);
    public const string Raft = nameof(Raft);
    public const string EnemyIsland = nameof(EnemyIsland);
}
