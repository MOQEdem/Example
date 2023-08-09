using System;
using UnityEngine;

public class IslandLoader : MonoBehaviour
{
    [SerializeField] private IslandLoadController _islandLoadController;
    [SerializeField] private string _sceneName;

    public event Action Visit; 

    public void OnButtonClick()
    {
        Visit?.Invoke();
        _islandLoadController.StartLoadMap(_sceneName);
    }
}