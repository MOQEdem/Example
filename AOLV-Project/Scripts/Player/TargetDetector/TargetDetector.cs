using UnityEngine;
using UnityEngine.Events;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private VisibilityRange _visibilityRange;

    public event UnityAction<Unit> Detected;

    private void OnEnable()
    {
        _visibilityRange.TargetDetected += OnTargetDetected;
    }

    private void OnDisable()
    {
        _visibilityRange.TargetDetected-= OnTargetDetected;    
    }

    private void OnTargetDetected(Unit unit)
    {
        Detected?.Invoke(unit);
    }

    private void OnTargetLossed()
    {      
    }   
}