using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IslandGeneratorData", menuName = "GameAssets/IslandGeneratorData")]
public class IslandGeneratorData : ScriptableObject
{
    [SerializeField] private GameDifficultyData[] _gameDifficultyData;

    public GameDifficultyData GetGameDifficultySettings(int difficultyLevel)
    {
        if (difficultyLevel > _gameDifficultyData.Length - 1)
        {
            difficultyLevel = _gameDifficultyData.Length - 1;
        }

        return _gameDifficultyData[difficultyLevel];
    }
}

[Serializable]
public class GameDifficultyData
{
    [SerializeField] private Unit[] _resourcesSource;
    [SerializeField] private Unit[] _enemies;
    [SerializeField] private int _mapSize;

    public Unit[] Resources => _resourcesSource;
    public Unit[] Enemies => _enemies;
    public int MapSize => _mapSize;
}
