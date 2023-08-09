using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionPower;
    [SerializeField] private Transform _parts;
    [SerializeField] private Renderer[] _meshRenderers;
    [SerializeField] private ParticleSystem _bloodEffect;

    private bool _isExploded = false;
    private Rigidbody[] _rigidbodies;

    public event Action<Transform> Activated;

    private void Awake()
    {
        _rigidbodies = _parts.GetComponentsInChildren<Rigidbody>();
        _parts.gameObject.SetActive(false);
    }

    private void RemoveParts()
    {
        _isExploded = true;
        _parts.SetParent(null);
        _parts.gameObject.SetActive(true);
        Destroy(_parts.gameObject, 3F);
        foreach (var mesh in _meshRenderers)
        {
            mesh.enabled = false;
        }
    }

    private void Explode()
    {
        if (_isExploded)
            return;

        RemoveParts();

        foreach (var rigidbody in _rigidbodies)
        {
            Vector3 force = (rigidbody.transform.position - (transform.position + new Vector3(0, 0, -0.5f))).normalized * _explosionPower;
            rigidbody.isKinematic = false;
            rigidbody.AddForceAtPosition(force, transform.position, ForceMode.Impulse);
        }

        var effect = Instantiate(_bloodEffect, transform.position + Vector3.up * 0.8f, Quaternion.identity);
        Activated?.Invoke(effect.transform);
        gameObject.SetActive(false);
    }

    private void Explode(Vector3 position, float force, float radius)
    {
        if (_isExploded)
            return;

        RemoveParts();

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(force, position, radius);         
        }

        var effect = Instantiate(_bloodEffect, transform.position + Vector3.up * 0.8f, Quaternion.identity);
        Activated?.Invoke(effect.transform);
        gameObject.SetActive(false);
    }

    public void Activate(Vector3 attackPosition)
    {
        Explode();
    }

    public void Activate(Vector3 position, float force, float radius)
    {
        Explode(position, force, radius);
    }
}