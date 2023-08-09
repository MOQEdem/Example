using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Character : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] protected CharacterType Type;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected int MaxHealthRandomizationValue;
    [SerializeField] protected int Damage;
    [SerializeField] protected int DamageRandomizationValue;
    [SerializeField] protected float AttackDelay;
    [Space]
    [Header("Visual")]
    [SerializeField] private ParticleSystem _hitParticle;

    protected Health Health;
    protected CharacterAudio Audio;
    protected CharacterAnimator Animator;
    protected Character Target;
    protected bool IsAttack = true;
    protected float LastAttackTime;
    protected TargetDetector TargetDetector;
    protected bool IsBattleEnded = false;
    protected virtual Transform ResurrectionPoint() { return null; }

    protected bool _isDead;
    private Color _startColor;
    private Material[] _characterMaterial;

    public bool IsReadyToFight => !IsBattleEnded;
    public bool IsDead => _isDead;
    public CharacterType CharacterType => Type;
    public float HealthFullness => Health.HealthFullness;

    public Action<Character> Died;
    public event Action Resurrected;
    public event Action HealthUpdate;

    protected virtual void Awake()
    {
        int healthOffset = UnityEngine.Random.Range(-MaxHealthRandomizationValue, MaxHealthRandomizationValue + 1);
        Health = new Health(MaxHealth + healthOffset);

        int damageOffset = UnityEngine.Random.Range(-DamageRandomizationValue, DamageRandomizationValue + 1);
        Damage += damageOffset;

        Animator = GetComponent<CharacterAnimator>();
        _characterMaterial = GetComponentInChildren<SkinnedMeshRenderer>().materials;
        _startColor = _characterMaterial[0].color;
        TargetDetector = GetComponentInChildren<TargetDetector>();
        Audio = GetComponentInChildren<CharacterAudio>();
    }

    protected virtual void OnEnable()
    {
        Health.HealthLost += Die;
        Health.HealthUpdated += OnHealthUpdated;
        TargetDetector.TargetFound += OnTargetFound;
    }

    protected virtual void OnDisable()
    {
        Health.HealthLost -= Die;
        Health.HealthUpdated -= OnHealthUpdated;
        TargetDetector.TargetFound -= OnTargetFound;
    }

    protected virtual void Start()
    {
        OnHealthUpdated();
    }

    public void Destroy()
    {
        this.ApplyDamage(Health.MaxHealth);
    }

    public void EndBattleStatus()
    {
        IsBattleEnded = true;
    }

    public virtual void Resurrect()
    {
        Animator.Resurrect();
        Resurrected?.Invoke();
        transform.position = ResurrectionPoint().position;
        Health.IncreaseHealth(1);
        _isDead = false;
    }

    protected virtual void OnTargetFound() { }

    protected virtual void Attack()
    {
        if (Target != null)
        {
            Animator.Attack();
            Audio.Attack();
            Target.ApplyDamage(Damage);
        }
    }

    protected virtual void AttackAllTargets(int damage)
    {
        if (TargetDetector.Targets.Count > 0)
        {
            Character[] characters = TargetDetector.Targets.ToArray();

            foreach (var unit in characters)
            {
                if (unit != null && !unit.IsDead)
                    unit.ApplyDamage(damage);
            }
        }
    }

    protected Character TryGetNextTarget()
    {
        if (TargetDetector.Targets.Count == 0)
            return null;

        OnTargetFound();

        return Target;
    }

    protected virtual void Die()
    {
        Died?.Invoke(this);
        _isDead = true;
        IsAttack = false;
        Animator.Die();
        Audio.Die();
    }

    protected void OnHealthUpdated()
    {
        HealthUpdate?.Invoke();
    }

    public virtual void ApplyDamage(int damage)
    {
        Health.ApplyDamage(damage);
        _hitParticle.Play();
        _characterMaterial[0].color = Color.red;
        _characterMaterial[0].DOColor(_startColor, 0.6f);
    }
}

public enum CharacterType
{
    Player,
    Enemy
}