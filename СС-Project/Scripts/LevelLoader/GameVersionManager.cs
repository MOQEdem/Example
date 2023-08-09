using System;
using UnityEngine;

public class GameVersionManager : MonoBehaviour
{
    [SerializeField] private GameVersion _buildVersion;

    private SavedVersion _savedVersion;

    public bool IsLoadedOldVersion { get; private set; }

    public event Action OldVersionLoaded;

    private void Awake()
    {
        _savedVersion = new SavedVersion();
        _savedVersion.Load();
    }

    private void Start()
    {
        if (_buildVersion > _savedVersion.Version)
        {
            IsLoadedOldVersion = true;
            OldVersionLoaded?.Invoke();
            _savedVersion.SaveNewVersion(_buildVersion);
        }
        else
        {
            IsLoadedOldVersion = false;
        }
    }
}

[Serializable]
public class SavedVersion : SavedObject<SavedVersion>
{
    [SerializeField] private GameVersion _version;

    private const string SaveKey = nameof(SavedVersion);

    public GameVersion Version => _version;

    public SavedVersion() : base(SaveKey)
    {
        _version = GameVersion.Alfa;
    }

    public void SaveNewVersion(GameVersion version)
    {
        _version = version;
        Save();
    }

    protected override void OnLoad(SavedVersion loadedObject)
    {
        _version = loadedObject.Version;
    }
}

[Serializable]
public enum GameVersion
{
    Alfa,
    Ver1
}


