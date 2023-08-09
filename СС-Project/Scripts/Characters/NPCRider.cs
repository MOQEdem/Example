using System.Collections;
using UnityEngine;

public class NPCRider : NPC
{
    [Space]
    [Header("NPC Characteristics")]
    [SerializeField] protected float AgroRadius = 5;
    [SerializeField] protected float AttackRange = 1;
    [SerializeField] protected float StoppingOffSet = 0.4f;
    [SerializeField] protected bool IsHaveMount = true;

    private Collider _collider;
    private Material _material;
    private Rigidbody _rigidbody;
    private NPCWalker _mount;

    protected override void Awake()
    {
        base.Awake();

        if (IsHaveMount)
            _mount = GetComponentInParent<NPCWalker>();

        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        //if (Renderer != null)
        //   _material = Renderer.material;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (IsHaveMount)
            _mount.Died += OnMountDied;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (IsHaveMount)
            _mount.Died -= OnMountDied;
    }

    private void OnMountDied(Character mount)
    {
        this.Die();
    }

    protected override void Die()
    {
        transform.parent = null;
        TryDropResource();
        base.Die();

        StartCoroutine(DestroyWhithDelay());
        _collider.enabled = false;
    }


    protected void CheckTargetDistance()
    {
        if (Target == null)
        {
            if (TargetDetector.Targets.Count > 0)
            {
                foreach (var target in TargetDetector.Targets)
                {
                    if ((target.transform.position - this.transform.position).sqrMagnitude < AgroRadius * AgroRadius)
                    {
                        OnTargetFound();
                        break;
                    }
                }
            }
        }
        else
        {
            if ((transform.position - this.transform.position).sqrMagnitude > AgroRadius * AgroRadius)
            {
                Target = null;
            }
        }
    }

    private IEnumerator DestroyWhithDelay()
    {
        var deathDelay = 3f;
        var deathAnimationDuration = 1.2f;

        yield return new WaitForSeconds(deathAnimationDuration);

        // float currentDissolveValue = 0;

        //while (_material.GetFloat("_DissolveAmount") < 1)
        //{
        //    currentDissolveValue += Time.deltaTime;
        //    _material.SetFloat("_DissolveAmount", currentDissolveValue);
        //    yield return null;
        //}

        _rigidbody.isKinematic = false;
        _rigidbody.drag = 0;
        _rigidbody.AddForce(Vector3.down, ForceMode.Impulse);
        Destroy(gameObject, deathDelay);
    }
}
