using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Island : SavedObject<Island>
{
    [SerializeField] private int _cooldown;

    public int Cooldown => _cooldown;
    
    public Island(string guid) : base(guid) {}

    public void SetCooldown()
    {
        _cooldown = Random.Range(2,5);
    }

    protected override void OnLoad(Island loadedObject)
    {
        if (loadedObject.Cooldown > 0)
            _cooldown = loadedObject.Cooldown - 1;
        else
            _cooldown = loadedObject.Cooldown;
    }
}