using UnityEngine;

public class TargetRayCastChecker : MonoBehaviour
{
    [SerializeField] private float _maxCheckingRadius = 3f;
    [SerializeField] private float _minchekingRadius = 1.3f;
    [SerializeField] private Transform _chekingCenter;
    [SerializeField] private LayerMask _layerMask;

    private float _checkingRadius;

    private void Awake()
    {
        SetMaxChekingRadius();
    }

    public bool TryCheckTarget()
    {
        return Physics.Raycast(_chekingCenter.position, Vector3.down, _checkingRadius, _layerMask);
    }

    public void SetMinChekingRadius() 
    {
        _checkingRadius = _minchekingRadius;
    } 

    public void SetMaxChekingRadius() 
    {
        _checkingRadius = _maxCheckingRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_chekingCenter.position, _chekingCenter.position - new Vector3(0, _maxCheckingRadius, 0));
    }
}
