using System;
using UnityEngine;

[Serializable]
public class IslandCounter : SavedObject<IslandCounter>
{
    private const string SaveKey = nameof(IslandCounter);

    [SerializeField] private int _islandNumber = 0;

    public int IslandNumber => _islandNumber;

    public IslandCounter()
        : base(SaveKey)
    { }

    public void AddIsland()
    {
        _islandNumber++;
        Save();
    }

    protected override void OnLoad(IslandCounter loadedObject)
    {
        _islandNumber = loadedObject.IslandNumber;
    }
}
