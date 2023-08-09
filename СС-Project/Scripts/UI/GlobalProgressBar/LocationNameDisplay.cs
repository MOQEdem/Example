using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class LocationNameDisplay : MonoBehaviour
{
    [SerializeField] private LocationName[] _levels;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private StartCameraView _startCamera;

    private GameProgress _progress;

    private void Start()
    {
        _name.text = _startCamera.LevelName;

        ShowLevelNumber();
        ShowLocationIcon();
    }

    private void ShowLevelNumber()
    {
        _progress = new GameProgress();
        _progress.Load();
        _number.text = _progress.CurrentLevel.ToString();
    }

    private void ShowLocationIcon()
    {
        foreach (LocationName location in _levels)
        {
            if (location.Name == _name.text)
            {
                location.EnableIcon();
                break;
            }
        }
    }
}

[Serializable]
public class LocationName
{
    [SerializeField] private string _name;
    [SerializeField] private Image _locationIcon;

    public string Name => _name;

    public void EnableIcon()
    {
        _locationIcon.gameObject.SetActive(true);
    }
}