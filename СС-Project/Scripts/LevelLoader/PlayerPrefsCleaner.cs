using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsCleaner : MonoBehaviour
{
    private PlayerPrefManager _manager = new PlayerPrefManager();
    private Button _clearButton;

    private void Awake()
    {
        _clearButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _clearButton.onClick.AddListener(ClearAllPrefs);
    }

    private void OnDisable()
    {
        _clearButton.onClick.RemoveListener(ClearAllPrefs);
    }

    private void ClearAllPrefs()
    {
        _manager.ClearAllPrefs();
    }
}
