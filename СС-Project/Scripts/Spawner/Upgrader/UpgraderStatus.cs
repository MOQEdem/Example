using System;
using UnityEngine;

[Serializable]
public class UpgraderStatus : SavedObject<UpgraderStatus>
{
    [SerializeField] private UpgraderLevelHolder _status = new UpgraderLevelHolder();

    public UpgraderStatus(string SaveKey)
        : base(SaveKey)
    { }

    public UpgraderLevel GetCurrentUpgradeLevel()
    {
        return _status.Level;
    }

    public void LevelUp()
    {
        if (UpgraderLevel.Highest > _status.Level)
            _status.LevelUp();
    }

    protected override void OnLoad(UpgraderStatus loadedObject)
    {
        _status = loadedObject._status;
    }
}

[Serializable]
public class UpgraderLevelHolder
{
    [SerializeField] private UpgraderLevel _level = UpgraderLevel.Base;

    public UpgraderLevel Level => _level;

    public void LevelUp()
    {
        if (UpgraderLevel.Highest > _level)
            _level++;
    }
}

[Serializable]
public enum UpgraderLevel
{
    Base,
    Intermediate,
    Advanced,
    Proficiency,
    Highest
}
