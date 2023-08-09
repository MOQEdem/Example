using UnityEngine;
using DG.Tweening;

public class ShootState : AttackState
{
    [SerializeField] private BowShooter _bow;
    [SerializeField] private bool _isRotateToTarget = false;  
  
    protected override void AttackTarget(Unit target)
    {
        _bow.SootEnded += OnShootEnded;
        _bow.Attack(target);
        if (_isRotateToTarget == true)
        {
            RotateToTarget(target.transform);
        }
    }   

    private void RotateToTarget(Transform target)
    {
        float angle = 75f;
        float rotationTime = 0.4f;
        Vector3 shootTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = shootTarget - transform.position;
        Quaternion targetQuternion = Quaternion.LookRotation(direction, transform.up);
        transform.DORotate(targetQuternion.eulerAngles  + transform.up* angle, rotationTime);
    }

    private void OnShootEnded()
    {
        _bow.SootEnded -= OnShootEnded;
        StartReloadAttack();
    }
}
