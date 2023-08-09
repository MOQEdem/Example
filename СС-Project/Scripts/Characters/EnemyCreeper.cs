using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyCreeper : EnemyNPC
{
    [Space]
    [Header("CreeperStaff")]
    [SerializeField] private int _explosionDamage;
    [SerializeField] private ParticleSystem[] _explosions;

    private bool _isWaitToExplode = true;
    private Coroutine _exploding;

    protected override void FixedUpdate()
    {
        if (!_isWaitToExplode)
            return;

        base.FixedUpdate();
    }

    protected override void DoAttack()
    {
        Animator.Run(false);
        Mover.NavMeshAgent.stoppingDistance = AttackRange;
        Mover.SetSpeedMove(0);

        if (_exploding == null)
            _exploding = StartCoroutine(Exploding());

        _isWaitToExplode = false;
    }

    protected override void Die()
    {
        base.Die();

        Audio.Die();

        if (_exploding != null)
            StopCoroutine(_exploding);
    }

    private IEnumerator Exploding()
    {
        Tween scaling = transform.DOScale(1.1f, 0.2f);
        yield return scaling.WaitForCompletion();
        scaling = transform.DOScale(1f, 0.2f);
        yield return scaling.WaitForCompletion();
        scaling = transform.DOScale(1.1f, 0.2f);
        yield return scaling.WaitForCompletion();
        scaling = transform.DOScale(1f, 0.2f);
        yield return scaling.WaitForCompletion();

        Renderer.enabled = false;

        foreach (var explosion in _explosions)
        {
            explosion.transform.parent = null;
            explosion.Play();
            Destroy(explosion.gameObject, 5f);
        }

        Audio.Attack();

        AttackAllTargets(_explosionDamage);
        Die();
    }
}
