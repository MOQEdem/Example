using System;
using UnityEngine;

[Serializable]
public class GameProgress : SavedObject<GameProgress>
{
    [SerializeField] private int _currentLevel = 1;

    private const string SaveKey = nameof(GameProgress);

    private int numberOfBaseLevels = 25;
    private int numberOfLoopedLevels = 6;

    public int CurrentLevel => _currentLevel;

    public GameProgress()
        : base(SaveKey)
    { }

    public int GetCurrentLevelToLoad()
    {
        if (_currentLevel > numberOfBaseLevels)
        {
            int loopedLevelDone = _currentLevel - numberOfBaseLevels;

            int loopedLevelToLoadIndex = loopedLevelDone % numberOfLoopedLevels;

            return numberOfBaseLevels + loopedLevelToLoadIndex;
        }
        else
        {
            return _currentLevel;
        }
    }

    public void SetCurrentLevel(int levelIndex)
    {
        _currentLevel = levelIndex;
        Save();
    }

    protected override void OnLoad(GameProgress loadedObject)
    {
        _currentLevel = loadedObject.CurrentLevel;
    }
}
