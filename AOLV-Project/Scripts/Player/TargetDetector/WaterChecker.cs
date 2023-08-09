using UnityEngine;

public class WaterChecker : MonoBehaviour
{
    public bool IsWaterDetected { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent( out Water water))
        {
            ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);
            IsWaterDetected = true;          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        if (other.TryGetComponent(out Water water))
        {
            IsWaterDetected = false;           
        }        
    }
}
