using DG.Tweening;
using System.Collections;
using UnityEngine;

public class NPCWalker : NPC
{
    [Space]
    [Header("NPC Characteristics")]
    [SerializeField] protected bool IsElite;
    [SerializeField] protected float AgroRadius = 5;
    [SerializeField] protected float AttackRange = 1;
    [SerializeField] protected float StoppingOffSet = 0.4f;

    protected Collider _collider;
    private Rigidbody _rigidbody;
    private Coroutine _jumping;
    private Material _material;
    private PlayerCharacterBuffer _buffer;
    private float sqrAgroRadius;

    protected Player Player;
    protected CharacterMove Mover;
    protected Vector3 DefaultPosition;

    public bool IsEliteNPC => IsElite;
    public CharacterMove CharacterMove => Mover;

    protected override void Awake()
    {
        base.Awake();

        Mover = GetComponent<CharacterMove>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _buffer = GetComponent<PlayerCharacterBuffer>();

        sqrAgroRadius = AgroRadius * AgroRadius;

        if (Renderer != null)
            _material = Renderer.material;
    }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    public void ChangeSkill(int additionalHealth, int additionalDamage, float scale)
    {
        transform.localScale = transform.localScale += new Vector3(scale, scale, scale);
        Health.ChangeMaxValue(additionalHealth);
        Damage += additionalDamage;
    }

    public void ShowArmor(Material material)
    {
        if (_buffer != null)
        {
            Audio.Resurrect();
            _buffer.ShowArmor(material);
        }
    }

    public override void ApplyDamage(int damage)
    {
        if (_jumping == null && !IsElite)
            _jumping = StartCoroutine(Jumping());

        base.ApplyDamage(damage);
    }

    public void DoHitJump()
    {
        if (_jumping == null)
            _jumping = StartCoroutine(Jumping());
    }

    protected override void Die()
    {
        base.Die();
        TryDropResource();
        Mover.ChangeMoveState(false);
        StartCoroutine(DestroyWhithDelay());
        _collider.enabled = false;
    }

    public void SetTargetPosition(Vector3 target)
    {
        Mover.SetGoalToMove(target);
    }


    public void SetDefaultPosition(Vector3 target)
    {
        DefaultPosition = target;
    }

    public void MoveToStartPosition()
    {
        Mover.SetGoalToMove(DefaultPosition);
    }

    protected void MoveToDefaultPosition()
    {
        if (Target == null)
        {
            if (TargetDetector.Targets.Count > 0)
            {
                foreach (var target in TargetDetector.Targets)
                {
                    if ((target.transform.position - DefaultPosition).sqrMagnitude < sqrAgroRadius)
                    {
                        Mover.SetSpeedMove(Mover.Speed);
                        OnTargetFound();
                        break;
                    }
                }
            }

            if (Target == null && (transform.position - DefaultPosition).sqrMagnitude > StoppingOffSet * StoppingOffSet)
            {
                Mover.SetGoalToMove(DefaultPosition);
                Mover.NavMeshAgent.stoppingDistance = StoppingOffSet;
            }
        }
        else
        {
            if ((transform.position - DefaultPosition).sqrMagnitude > sqrAgroRadius)
            {
                Target = null;

                Mover.SetSpeedMove(Mover.Speed);
                Mover.SetGoalToMove(DefaultPosition);
                Mover.NavMeshAgent.stoppingDistance = 0;
            }
        }
    }

    private IEnumerator DestroyWhithDelay()
    {
        var deathDelay = 3f;
        var deathAnimationDuration = 1.2f;
        Mover.NavMeshAgent.enabled = false;

        yield return new WaitForSeconds(deathAnimationDuration);

        //  float currentDissolveValue = 0;

        //while (_material.GetFloat("_DissolveAmount") < 1)
        //{
        //    currentDissolveValue += Time.deltaTime;
        //    _material.SetFloat("_DissolveAmount", currentDissolveValue);
        //    yield return null;
        //}

        Mover.NavMeshAgent.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.drag = 0;
        _rigidbody.AddForce(Vector3.down, ForceMode.Impulse);
        Destroy(gameObject, deathDelay);
    }

    private IEnumerator Jumping()
    {
        Tween myTween = transform.DOJump(transform.position - transform.forward / 0.8f, 0.8f, 1, 0.3f);
        yield return myTween.WaitForCompletion();

        _jumping = null;
    }
}
