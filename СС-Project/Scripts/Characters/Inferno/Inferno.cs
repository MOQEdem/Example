using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Inferno : EnemyArcher
{
    [Space]
    [Header("Inferno Stuff")]
    [SerializeField] private int _numberOfShots;
    [SerializeField] private float _timeBetweenShots;

    private Coroutine _shooting;

    protected override void DoAttack()
    {
        if (_shooting == null)
            _shooting = StartCoroutine(Shooting());

        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);
        LastAttackTime = AttackDelay + (_numberOfShots * _timeBetweenShots);
    }

    private IEnumerator Shooting()
    {
        var delay = new WaitForSeconds(_timeBetweenShots);
        var currentAmmo = _numberOfShots;

        while (currentAmmo > 0)
        {
            if (Target != null)
            {
                transform.DOLookAt(Target.transform.position, 0.1f);
                Arrow arrow = ArrowsPool.First(a => a.gameObject.activeSelf == false);

                Animator.Attack();
                Audio.Attack();

                arrow.Shoot(Target, Damage);

                currentAmmo--;
                yield return delay;
            }
            else
            {
                currentAmmo = 0;
            }
        }

        _shooting = null;
    }
}
