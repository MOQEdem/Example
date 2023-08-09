using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGoalMapper : MonoBehaviour
{
    [SerializeField] private LevelIcons[] _level;
    [SerializeField] private Transform _startBarPoint;
    [SerializeField] private Transform _finishBarPoint;
    [SerializeField] private CompleteIconChanger _completeIconChanger;
    private List<int> _distances = new List<int>();
    private float _allEnemiesCount;
    private float _barDistance;
    private float _percentageRatio = 100;

    public void Enable()
    {
        _barDistance = Math.Abs(_startBarPoint.position.x - _finishBarPoint.position.x);
        ShowCurrentLevelIcons(); 
    }

    public void AddDistance(int distance)
    {    
        _distances.Add(distance);
        _allEnemiesCount += distance;
    }

    private void ShowCurrentLevelIcons()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        int iconCount = _level[currentLevel].GetIconsCount();
        float barPercent = _barDistance / _percentageRatio;
        int calculatedDistance = 0;

        for (int i = 0; i < iconCount; i++)
        {
            Image icon = _level[currentLevel].GetIcon(i);
            icon.enabled = true;
            _completeIconChanger.AddIcon(icon);
            
            if(i == 0)
            {
                calculatedDistance = _distances[i];
            }
            else if(i > 0)
            {
                Transform previousIcon = _level[currentLevel].GetIcon(i - 1).gameObject.transform;
                   
                previousIcon.transform.position = new Vector3(
                previousIcon.transform.position.x -
                (barPercent * (_percentageRatio - (calculatedDistance / (_allEnemiesCount / _percentageRatio)))),
                previousIcon.transform.position.y,
                previousIcon.transform.position.z);
                calculatedDistance = _distances[i - 1] + _distances[i];
            }
        }
    }
}

[Serializable]
public class LevelIcons
{
    [SerializeField] private Image[] _icons;

    public int GetIconsCount()
    {
        return _icons.Length;
    }

    public Image GetIcon(int number)
    {
        return _icons[number];
    }
}