using System;
using UnityEngine;

[Serializable]
public class PlayerSquadLevel : SavedObject<PlayerSquadLevel>
{
    private const string SaveKey = nameof(PlayerSquadLevel);

    [SerializeField] private int _level = 0;

    public int Level => _level;

    public PlayerSquadLevel()
        : base(SaveKey)
    { }

    public void LevelUp()
    {
        _level++;
    }

    protected override void OnLoad(PlayerSquadLevel loadedObject)
    {
        _level = loadedObject.Level;
    }
}
