using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GlobalProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private SectorView[] _sectors;
    [SerializeField] private string[] _locationNames;
    [SerializeField] private Image[] _currentlocationIcons;
    [SerializeField] private Image[] _nextlocationIcons;
    [SerializeField] private GameObject[] _currentLocationSteps;
    [SerializeField] private int _highLvlThreshold = 25;
    [SerializeField] private int _infinityLevelsThreshold = 31;

    private List<Image> _locationImages = new List<Image>();
    private GameProgress _progress;
    private string _currentLevelName;
    private string _end = "End";
    private Color _enabledColor = new Color(255, 255, 255, 1);

    public void SetHeaderText(string text)
    {
        _currentLevelName = text;
        _headerText.text = _currentLevelName;
    }

    public void SetCurrentView(int progressLevel)
    {
        _progress = new GameProgress();
        _progress.Load();

        if (_progress.CurrentLevel <= _infinityLevelsThreshold)
        {
            if (SceneManager.GetActiveScene().buildIndex > _highLvlThreshold)
                _currentLevelName += _end;

            SetLocationIcons();

            for (int i = 0; i < progressLevel; i++)
                _locationImages[i].color = _enabledColor;
        }  
    }

    private void SetLocationIcons()
    {
        for (int i = 0; i < _locationNames.Length; i++)
        {
            if (_locationNames[i] == _currentLevelName)
            {
                _currentlocationIcons[i].gameObject.SetActive(true);
                _nextlocationIcons[i].gameObject.SetActive(true);

                _currentLocationSteps[i].gameObject.SetActive(true);
                _locationImages = _currentLocationSteps[i].gameObject.GetComponentsInChildren<Image>().ToList();
                break;
            }
        }
    }
}