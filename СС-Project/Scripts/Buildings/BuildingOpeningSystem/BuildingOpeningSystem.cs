using System.Collections.Generic;
using UnityEngine;

public class BuildingOpeningSystem : MonoBehaviour
{
    [SerializeField] private PlayerSquad _playerSquad;
    [SerializeField] private BuildingOpeningRequirements _requirements;

    private BaseMover _mover;
    private Building[] _buildings;
    private List<BuildingData> _availableBuildings;

    private void Awake()
    {
        _mover = GetComponentInParent<BaseMover>();
        _buildings = GetComponentsInChildren<Building>(true);
        _availableBuildings = _requirements.GetListOfAvailableBuildings();
    }

    private void OnEnable()
    {
        _playerSquad.NewLevelSet += RunCheckAvailability;
        _mover.ProgressSaved += SaveBuildingsStatus;
    }

    private void OnDisable()
    {
        _playerSquad.NewLevelSet -= RunCheckAvailability;
        _mover.ProgressSaved -= SaveBuildingsStatus;
    }

    private void SaveBuildingsStatus()
    {
        //  foreach (var building in _buildings)
        //     building.SaveStatus();
    }

    private void RunCheckAvailability(int playerLevel)
    {
        foreach (var building in _buildings)
            if (building != null)
                SetBuildingAvailability(building, playerLevel);
    }

    private void SetBuildingAvailability(Building building, int playerLevel)
    {
        foreach (BuildingData data in _availableBuildings)
        {
            if (data.Type == building.Type)
            {
                building.gameObject.SetActive(true);
                building.CheckBuildability(data.MinPlayerLevel);

                return;
            }
        }

        building.gameObject.SetActive(false);
    }
}
