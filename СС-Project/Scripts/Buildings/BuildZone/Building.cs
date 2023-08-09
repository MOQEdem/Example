using System;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] private BuildingTypes _type;

    private BuildableObject _object;
    private BuildZone _buildZone;
    private BuildingOpener _opener;

    private BuildingStatus _buildingStatus;

    protected abstract BuildingStatus InitBuildingStatus();

    public BuildingTypes Type => _type;
    public bool IsBuilt => _buildingStatus.IsBuilded;

    public event Action Built;

    private void Awake()
    {
        _opener = GetComponent<BuildingOpener>();
        _object = GetComponentInChildren<BuildableObject>();
        _buildZone = GetComponentInChildren<BuildZone>();

        _buildingStatus = InitBuildingStatus();
        _buildingStatus.Load();

        if (_buildZone != null)
            SetObjectActivity();
    }

    private void OnEnable()
    {
        if (_buildZone != null && !_buildingStatus.IsBuilded)
            _buildZone.Build += OnBuild;
    }

    private void OnDisable()
    {
        if (_buildZone != null && !_buildingStatus.IsBuilded)
            _buildZone.Build -= OnBuild;
    }

    public void SaveStatus()
    {
        _buildingStatus.Save();
    }

    public void CheckBuildability(int RequiredLevel)
    {
        if (_opener != null)
            _opener.CheckBuildability(RequiredLevel);
    }

    private void SetObjectActivity()
    {
        _object.gameObject.SetActive(_buildingStatus.IsBuilded);
        _buildZone.gameObject.SetActive(!_buildingStatus.IsBuilded);
    }

    private void OnBuild()
    {
        _buildingStatus.SetBuildedStatus();
        _buildZone.Build -= OnBuild;

        SetObjectActivity();

        Built?.Invoke();
    }
}
