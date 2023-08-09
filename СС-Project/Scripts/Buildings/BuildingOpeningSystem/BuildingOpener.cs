using UnityEngine;

[RequireComponent(typeof(Building))]
public class BuildingOpener : MonoBehaviour
{
    [SerializeField] private Building _requireBuilding;
    [SerializeField] private UpgradeZone _squad;

    private BuildingOpenerView _view;
    private Building _building;
    private BuildZone _zone;
    private int _requiredLevel;

    private void Awake()
    {
        _view = GetComponentInChildren<BuildingOpenerView>();
        _building = GetComponent<Building>();
        _zone = GetComponentInChildren<BuildZone>();
    }

    public void CheckBuildability(int RequiredLevel)
    {
        _requiredLevel = RequiredLevel;

        if (!_building.IsBuilt)
        {
            if (!_requireBuilding.IsBuilt)
            {
                _zone.gameObject.SetActive(false);
                _view.gameObject.SetActive(false);
                _requireBuilding.Built += OnRequireBuildingBuilt;
            }
            else
            {
                if (_requiredLevel > _squad.SquadLevel)
                {
                    _view.SetRequiredLevelValue(RequiredLevel);
                    _view.gameObject.SetActive(true);
                    _zone.gameObject.SetActive(false);
                }
                else
                {
                    _view.gameObject.SetActive(false);
                    _zone.gameObject.SetActive(true);
                    _zone.CheckResourceCount();
                }
            }
        }
        else
        {
            _view.gameObject.SetActive(false);
        }
    }

    private void OnRequireBuildingBuilt()
    {
        CheckBuildability(_requiredLevel);
        _requireBuilding.Built -= OnRequireBuildingBuilt;
    }
}
