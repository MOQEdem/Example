using System;
using UnityEngine;

[Serializable]
public class Resource : SavedObject<Resource>
{
    [SerializeField] private int _value;
    [SerializeField] private ResourceType _type;

    public Resource(string guid, ResourceType type)
        : base(guid)
    {
        _type = type;
    }

    public event Action Changed;

    public int Value => _value;
    public ResourceType Type => _type;

    public void Add(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value += value;
        Changed?.Invoke();
    }

    public void Spend(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _value -= value;

        if (_value < 0)
            _value = 0;

        Changed?.Invoke();
    }

    protected override void OnLoad(Resource loadedObject)
    {
        _value = loadedObject._value;
        _type = loadedObject._type;
    }
}

[Serializable]
public enum ResourceType
{
    Gold,
    Wood,
    Stone,
    Iron,
    Plank,
    Food
}