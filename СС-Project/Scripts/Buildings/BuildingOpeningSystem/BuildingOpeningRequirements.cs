using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BuildingOpeningRequirements", menuName = "GameAssets/BuildingOpeningRequirements")]
public class BuildingOpeningRequirements : ScriptableObject
{
    [SerializeField] private BuildingData[] _buildings;

    public List<BuildingData> GetListOfAvailableBuildings()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        List<BuildingData> availableBuildings = new List<BuildingData>();

        foreach (var building in _buildings)
        {
            if (building.MinGameLevel <= currentLevel)
            {
                availableBuildings.Add(building);
            }
        }

        return availableBuildings;
    }

    public int GetDemandedLevel(BuildingTypes type)
    {
        foreach (var building in _buildings)
        {
            if (building.Type == type)
            {
                return building.MinGameLevel;
            }
        }

        return 0;
    }
}

[Serializable]
public class BuildingData
{
    [SerializeField] private BuildingTypes _type;
    [SerializeField] private int _minGameLevel;
    [SerializeField] private int _minPlayerLevel;

    public BuildingTypes Type => _type;
    public int MinGameLevel => _minGameLevel;
    public int MinPlayerLevel => _minPlayerLevel - 1;
}
