using System;
using UnityEngine;

public class RagdollStabilisator : MonoBehaviour
{
    private Rigidbody[] _rigidbodys;
    float maxSpeed = 5;
    float maxAngularSpeed = 5;

    public void SetRidgidbodies(Rigidbody[] rigidbodys)
    {
        _rigidbodys = rigidbodys;
    }

    public void Enable(Rigidbody[] rigidbodys)
    {
        _rigidbodys = rigidbodys;
        enabled = true;
    }

    private void FixedUpdate()
    {
        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.velocity = CheckVelocity(rigidbody.velocity, maxSpeed);
            rigidbody.angularVelocity = CheckVelocity(rigidbody.angularVelocity, maxAngularSpeed);
        }
    }

    private Vector3 CheckVelocity(Vector3 velocity, float maxSpeed)
    {
        return new Vector3(ClampValue(velocity.x, maxSpeed), ClampValue(velocity.y, maxSpeed), ClampValue(velocity.z, maxSpeed));
    }

    public void Disable()
    {
        enabled = false;
    }

    private float ClampValue(float value, float maxValue)
    {        
        return Mathf.Clamp(value, -maxValue, maxValue);
    }
}
