using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private CloadSave _cloadSave;

    private GameProgress _progress;

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();

        _cloadSave.LoadStartSceneSave();

        while (!_cloadSave.IsLoaded)
            yield return null;

        _progress = JsonUtility.FromJson<GameProgress>(_cloadSave.GetSavedData(SaveID.levelProgress));

        if (_progress == null || _progress.GetCurrentLevelToLoad() <= 0)
        {
            _progress = new GameProgress();

            var jsonString = JsonUtility.ToJson(_progress);
            _cloadSave.SetSavedData(SaveID.levelProgress, jsonString);
        }

        _progress.Save();

        SceneManager.LoadScene(_progress.GetCurrentLevelToLoad());
    }
}
