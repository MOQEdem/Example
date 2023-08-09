
using System;
using UnityEngine;

public interface IDieEffect 
{
    event Action<Transform> Activated;
    void Activate(Vector3 attackPosition);
    void Activate(Vector3 position, float force, float radius);
}


