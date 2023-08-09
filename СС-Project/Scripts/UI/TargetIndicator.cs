using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    private Transform _player;
    private Transform _target;

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - _player.transform.position;

            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(90, angle, 0);
        }
    }

    public void SetPoints(Transform player, Transform target)
    {
        _player = player;
        _target = target;
    }
}
