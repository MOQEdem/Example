using System;
using UnityEngine;

[RequireComponent(typeof(RagdollStabilisator))]
public class RagdollActivator : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 15f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _container;
    [SerializeField] private Rigidbody[] _rigidbodys;
    
    private RagdollStabilisator _ragdollStabilisator;
    private bool _isActivated = false;


    private void Awake()
    {
        _ragdollStabilisator = GetComponent<RagdollStabilisator>();
        _rigidbodys = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        _ragdollStabilisator.Disable();
        SetKinenaticState(true);
    }

    public void Activate(Vector3 attackPoint)
    {
        if (_isActivated == true)
            return;
        PreActivate();

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = false;
            Vector3 force = ((rigidbody.transform.position + transform.up*1.5f) - attackPoint).normalized * _forceMultiplier;
            rigidbody.AddForceAtPosition(force, transform.position + transform.up, ForceMode.VelocityChange);
        }        
    }

    public void SetKinenaticState(bool isActive)
    {
        _animator.enabled = isActive;
        SetIsKinematic(isActive);
        if (isActive == false)
            _ragdollStabilisator.Enable(_rigidbodys);
    }

    private void SetIsKinematic(bool isKinematic)
    {
        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = isKinematic;
        }
    }

    private void PreActivate()
    {
        _collider.enabled = false;
        _animator.enabled = false;
        _container.parent = null;
        _isActivated = true;
        Destroy(_container.gameObject, 5f);
    }
}
