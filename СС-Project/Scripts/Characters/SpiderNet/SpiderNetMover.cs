using UnityEngine;

public class SpiderNetMover : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _moveSpeed = 15;

    private Vector3 _target;
    
    private void Awake() => 
        enabled = false;

    public void MoveTo(Vector3 target)
    {
        _target = target;
        enabled = true;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards( transform.position, _target, _moveSpeed * Time.deltaTime);
        if (transform.position == _target)
            enabled = false;
    }
}
