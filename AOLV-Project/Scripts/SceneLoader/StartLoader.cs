using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoader : MonoBehaviour
{
    private TutorialStatus _tutorialStatus;

    private void Awake()
    {
        _tutorialStatus = new TutorialStatus();
        _tutorialStatus.Load();
        LoadStartLevel();
    }

    public void LoadStartLevel()
    {
        if (_tutorialStatus.IsTutorialComplite)
            SceneManager.LoadScene(SceneName.NewHub);
        else
            SceneManager.LoadScene(SceneName.Tutorial);
    }
}