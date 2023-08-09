using System;
using UnityEngine;

[Serializable]
public class PetStatus : SavedObject<PetStatus>
{
    private const string SaveKey = nameof(PetStatus);

    [SerializeField] private bool _isUnlocked = false;

    public bool IsUnlocked => _isUnlocked;

    public PetStatus()
        : base(SaveKey)
    { }

    public void Unlock()
    {
        _isUnlocked = true;
        Save();
    }

    protected override void OnLoad(PetStatus loadedObject)
    {
        _isUnlocked = loadedObject.IsUnlocked;
    }
}
