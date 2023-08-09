using UnityEngine;

[RequireComponent(typeof(ResourceAnimation))]
[RequireComponent(typeof(ResourceMover))]
public abstract class Resource : MonoBehaviour
{
    private ResourceType _type;
    private ResourceAnimation _animation;
    private ResourceMover _resourceMover;
    private AudioSource _sound;
    private Animator _animator;
    private ParticleSystem _particleSystem;

    private Vector3 _normalScale = Vector3.one;

    protected abstract ResourceType GetResourceType();

    public bool IsPickedUp { get; private set; }

    public ResourceType ResourceType => _type;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponentInChildren<Animator>();

        if (_animator != null)
            _animator.enabled = false;

        _animation = GetComponent<ResourceAnimation>();
        _resourceMover = GetComponent<ResourceMover>();
        _type = GetResourceType();
        _sound = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        transform.localScale = _normalScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourceCollector player) && IsPickedUp == false)
        {
            if (!player.PlayerStack.IsFull)
            {
                PickUp();
                player.PlayerStack.AddResourceToWaitList(this);
            }
        }
    }

    public void StopAllMovement()
    {
        _resourceMover.StopAllMovement();
    }

    public void PickUp()
    {
        _particleSystem.Stop();
        _animator.SetBool(ResourceAnimator.Bool.IsWaiting, false);
        _animator.enabled = false;
        _animator.transform.localPosition = Vector3.zero;
        _animator.transform.localRotation = Quaternion.Euler(Vector3.zero);

        IsPickedUp = true;
    }

    public void Drop(float height)
    {
        _sound.Play();
        _animation.PlayDropAnimation(height);

        _animator.enabled = true;
        _animator.SetBool(ResourceAnimator.Bool.IsWaiting, true);
        _particleSystem.Play();
    }

    public void SetTarget(Vector3 target)
    {
        _sound.Play();
        _resourceMover.SetTarget(target);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localScale = _normalScale;
    }

    private class ResourceAnimator
    {
        public class Bool
        {
            public const string IsWaiting = nameof(IsWaiting);
        }
    }
}

public enum ResourceType
{
    Player,
    Enemy
}
