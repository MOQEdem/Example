using System;
using UnityEngine;

public class BuildZone : InteractiveZone
{
    [SerializeField] private BuildableObject _building;

    public event Action Build;

    protected override void Activate()
    {
        Build?.Invoke();
    }
}
