using System;
using UnityEngine;

[Serializable]
public class BuildingStatus : SavedObject<BuildingStatus>
{
    [SerializeField] private bool _isBuilded = false;

    public BuildingStatus(string guid)
        : base(guid)
    { }

    public bool IsBuilded => _isBuilded;

    public void SetBuildedStatus()
    {
        _isBuilded = true;
    }

    protected override void OnLoad(BuildingStatus loadedObject)
    {
        _isBuilded = loadedObject._isBuilded;
    }
}
