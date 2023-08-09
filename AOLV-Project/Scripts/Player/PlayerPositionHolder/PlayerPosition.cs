using System;
using UnityEngine;

[Serializable]
public class PlayerPosition : SavedObject<PlayerPosition>
{
    [SerializeField] private Vector3 _position;

    public Vector3 Postion => _position;
    
    public PlayerPosition(string guid) : base(guid){}

    protected override void OnLoad(PlayerPosition loadedObject)
    {
        _position = loadedObject.Postion;
    }

    public void SavePosition(Vector3 position)
    {
        _position = position;
    }
}
