using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RaidGeneratorData", menuName = "GameAssets/RaidGeneratorData")]
public class RaidGeneratorData : ScriptableObject
{
    [SerializeField] private RaidGameDifficultyData[] _gameDifficultyData;

    public RaidGameDifficultyData GetGameDifficultySettings(int difficultyLevel)
    {
        if (difficultyLevel > _gameDifficultyData.Length - 1)
        {
            difficultyLevel = _gameDifficultyData.Length - 1;
        }

        return _gameDifficultyData[difficultyLevel];
    }
}

[Serializable]
public class RaidGameDifficultyData
{
    [SerializeField] private Unit[] _resourcesSource;
    [SerializeField] private Unit[] _objects;
    [SerializeField] private Unit[] _enemies;

    public Unit[] Resources => _resourcesSource;
    public Unit[] Objects => _objects;
    public Unit[] Enemies => _enemies;
}

