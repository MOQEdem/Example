using UnityEngine;

public class CameraLookPoint : MonoBehaviour
{
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private Joystick _joystick;

    private void Start()
    {
        transform.position = _lookPoint.transform.position;
    }

    private void Update()
    {
        if (_joystick.Direction == Vector2.zero)
            transform.position = Vector3.MoveTowards(transform.position, _lookPoint.transform.position, 10f * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, _lookPoint.transform.position, 10f * Time.deltaTime);
    }
}
