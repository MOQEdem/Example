using System;
using UnityEngine;

[Serializable]
public class GameDifficulty : SavedObject<GameDifficulty>
{
    private const string SaveKey = nameof(GameDifficulty);

    [SerializeField] private float _levelOfDifficulty = 0;

    public float LevelOfDifficulty => _levelOfDifficulty;

    public GameDifficulty()
        : base(SaveKey)
    { }

    public void AddDifficulty(float coefficient)
    {
        _levelOfDifficulty += coefficient;
    }

    protected override void OnLoad(GameDifficulty loadedObject)
    {
        _levelOfDifficulty = loadedObject.LevelOfDifficulty;
    }
}
