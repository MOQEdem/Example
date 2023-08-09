using Agava.YandexMetrica;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour
{
    private string _previosSceneName;

    public string PreviosSceneName => _previosSceneName;

    private void Awake()
    {
        _previosSceneName = PlayerPrefs.GetString("SceneName");
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        //YandexMetrica.Send("startNewIsland");
    }
}