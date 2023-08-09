using System;
using UnityEngine;

public class TriggerObserver : MonoBehaviour
{
    public event Action<Character> TargetFound;
    public event Action<Character> TargetLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
            TargetFound?.Invoke(character);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Character character))
            TargetLost?.Invoke(character);
    }
}
