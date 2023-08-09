using UnityEngine;

public class IslandLoadController : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    public void StartLoadMap(string sceneName)
    {
        _sceneLoader.LoadScene(sceneName);
    }
}
