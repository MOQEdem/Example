using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Heap : SavedObject<Heap>
{
    [SerializeField] private int _cooldown;

    public int Cooldown => _cooldown;

    public Heap(string guid) : base(guid){}
    
    public void SetCooldown()
    {
        _cooldown = Random.Range(1,4);
    }

    protected override void OnLoad(Heap loadedObject)
    {
        if (loadedObject.Cooldown > 0)
            _cooldown = loadedObject.Cooldown - 1;
        else
            _cooldown = loadedObject.Cooldown;
    }
}
