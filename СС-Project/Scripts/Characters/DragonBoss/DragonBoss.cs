using UnityEngine;

public class DragonBoss : EnemyNPC
{
    [Space]
    [Header("DragonsBoss Settings")]
    [SerializeField] private DragonFireAttack _dragonFireAttack;
    [SerializeField][Min(0)] private float _switchToMeleeDistance = 8;
    [SerializeField][Min(0)] private int _meleeAttackMaxTargetsCount = 3;
    [SerializeField][Min(0)] private float _meleeDamageRadius = 2;
    [SerializeField] private LayerMask _targetsLayer;

    private bool _isMelee;
    private DragonAnimator _dragonAnimator;
    private Collider[] _collidersBuffer;
    private DragonBossMeleeZone _dragonBossMeleeZone;

    protected override void Awake()
    {
        base.Awake();

        if (Animator is DragonAnimator dragonAnimator)
            _dragonAnimator = dragonAnimator;

        _collidersBuffer = new Collider[_meleeAttackMaxTargetsCount];

        _dragonBossMeleeZone = GetComponentInChildren<DragonBossMeleeZone>();
    }

    protected override void FixedUpdate()
    {
        if (_dragonBossMeleeZone.IsHaveTargets)
        {
            _isMelee = true;
        }
        else
        {
            _isMelee = false;
        }

        _dragonAnimator.SetFly(!_isMelee);

        base.FixedUpdate();
    }

    protected override void Attack()
    {
        if (Target)
        {
            transform.LookAt(Target.transform);
            Animator.Attack();
            Audio.Attack();
        }
    }

    public void MeleeAttack()
    {
        if (Target)
        {
            Physics.OverlapSphereNonAlloc(Target.transform.position, _meleeDamageRadius, _collidersBuffer, _targetsLayer);

            foreach (Collider other in _collidersBuffer)
                if (other != null)
                    if (other.TryGetComponent(out Character character) && character.CharacterType == CharacterType.Player)
                        character.ApplyDamage(Damage);
        }
    }

    public void SpawnFireBall()
    {
        if (Target)
            _dragonFireAttack.HitTarget(Target.transform.position);
    }
}