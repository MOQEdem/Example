using System.Collections;
using UnityEngine;

public abstract class NPC : Character
{
    [SerializeField] private ParticleSystem _dieParticle;
    [SerializeField] protected SkinnedMeshRenderer Renderer;
    [Space]
    [Header("Reward Options")]
    [SerializeField] private bool _isShouldDropResource = true;

    private Resource _reward;
    private bool _isResourceDropped = false;

    protected override void Awake()
    {
        base.Awake();

        _reward = GetComponentInChildren<Resource>();

        if (_reward != null)
            _reward.gameObject.SetActive(false);
    }

    protected override void Attack()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform);
        }

        base.Attack();
    }

    public void SetTarget(Character target)
    {
        Target = target;

        if (target == null)
            IsAttack = true;
    }

    protected override void OnTargetFound()
    {
        if (TargetDetector.Targets.Count == 1)
        {
            SetTarget(TargetDetector.Targets[0]);
        }
        else
        {
            Character character = TargetDetector.Targets[0];

            while (character is PlayableCharacter player && character != null)
            {
                int index = Random.Range(0, TargetDetector.Targets.Count);
                character = TargetDetector.Targets[index];
            }

            SetTarget(character);
        }
    }

    protected override void Die()
    {
        base.Die();

        if (_dieParticle != null)
        {
            StartCoroutine(ParticlePlay());
        }
    }

    private IEnumerator ParticlePlay()
    {
        _dieParticle.transform.SetParent(null);
        _dieParticle.Play();
        yield return new WaitForSeconds(3f);
        Destroy(_dieParticle.gameObject);
    }

    protected void TryDropResource()
    {
        if (_isShouldDropResource && !_isResourceDropped)
        {
            if (_reward != null)
            {
                _reward.gameObject.SetActive(true);

                _reward.SetParent(null);
                _reward.Drop(this.transform.position.y);
                _isResourceDropped = true;
            }
        }
    }
}
