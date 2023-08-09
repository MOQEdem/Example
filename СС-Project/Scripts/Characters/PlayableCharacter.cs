using UnityEngine;

public class PlayableCharacter : Character
{
    [SerializeField] private ParticleSystem _swordAttack;
    [Space]
    [Header("ResurrectionPoint")]
    [SerializeField] private Transform _resurrectionPoint;

    private PlayerMover _playerMover;

    protected override Transform ResurrectionPoint() => _resurrectionPoint;

    public int CurrentHealthValue => Health.CurrentHealth;
    public int CurrentDamageValue => Damage;
    public int MaxHealthValue => MaxHealth;

    protected override void Awake()
    {
        base.Awake();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if (TargetDetector.Targets.Count > 0)
        {
            foreach (var target in TargetDetector.Targets)
            {
                if (target.IsDead == false)
                {
                    Target = target;

                    if (LastAttackTime <= 0)
                    {
                        Attack();
                        Animator.Attack();
                        _swordAttack.Play();
                        LastAttackTime = AttackDelay;
                        return;
                    }
                }
            }

        }

        if (LastAttackTime > 0)
            LastAttackTime -= Time.deltaTime;
    }

    public void UpgradeHealth(int newHealth)
    {
        MaxHealth = newHealth;
        Health.SetNewHealthMaxValue(newHealth);
    }

    public void UpgradeDamage(int newDamage)
    {
        Damage = newDamage;
    }

    public void GetHeal(int healValue)
    {
        Health.IncreaseHealth(healValue);
    }

    public override void Resurrect()
    {
        base.Resurrect();
        _playerMover.enabled = true;
    }

    protected override void Die()
    {
        base.Die();
        _playerMover.enabled = false;
    }
}
