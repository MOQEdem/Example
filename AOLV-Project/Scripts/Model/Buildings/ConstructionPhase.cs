using System;
using UnityEngine;

[Serializable]
public class ConstructionPhase : SavedObject<ConstructionPhase>
{
    [SerializeField] private int _value = 0;

    public ConstructionPhase(string guid)
        : base(guid)
    { }

    public int Value => _value;

    public void SetNextPhase()
    {
        _value++;
        Save();
    }

    protected override void OnLoad(ConstructionPhase loadedObject)
    {
        _value = loadedObject.Value;
    }
}
