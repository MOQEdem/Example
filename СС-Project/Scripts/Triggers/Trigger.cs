using System;
using UnityEngine;

public class Trigger<T> : MonoBehaviour where T : MonoBehaviour
{
    public event Action<T> Enter;
    public event Action<T> Exit;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T triggered))
        {
            Enter?.Invoke(triggered);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T triggeredObject))
        {
            Exit?.Invoke(triggeredObject);
        }
    }
}
