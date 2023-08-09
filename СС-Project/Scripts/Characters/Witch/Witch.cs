using UnityEngine;

public class Witch : EnemyArcher
{
    [Space]
    [Header("Witch Setup")]
    [SerializeField] private bool _isNeedRunAwayAnimation;

    private const string _runAway = "RunAway";

    protected override void Die()
    {
        float deathDelay = 3f;

        if (_isNeedRunAwayAnimation)
        {
            Died?.Invoke(this);
            IsAttack = false;
            _isDead = true;
            Animator.SetCustomTrigger(_runAway);
            Audio.Die();
            Mover.ChangeMoveState(false);
            _collider.enabled = false;

            Destroy(this.gameObject, deathDelay);
        }
        else
        {
            base.Die();
        }
    }
}
