using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnedResource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private ResourceAnimation _takingAnimation;
    [SerializeField] private int _valueToGive;

    private ResourcePack _pack;
    private Player _player;
    private Collider[] _colliders;
    private Rigidbody _rigidbody;
    private bool _isPlayerSpending = false;
    private bool _isCollectedByPlayer = false;

    public ResourceType Type => _type;

    private void Awake()
    {
        _pack = new ResourcePack(_valueToGive, _type);
        _colliders = GetComponents<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _takingAnimation.Completed += OnAnimationCompleted;
    }

    private void OnDisable()
    {
        _takingAnimation.Completed -= OnAnimationCompleted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPlayerSpending == false && other.TryGetComponent<Player>(out Player player))
        {
            _player = player;
            DisablePhisics();

            _isCollectedByPlayer = true;

            _takingAnimation.TryStartAnimation(player.transform, true);
        }
    }

    public void StartSpendingAnimation(Transform targetPosition)
    {
        if (_isPlayerSpending)
            DisablePhisics();

        _takingAnimation.TryStartAnimation(targetPosition, true);
    }

    public void SetPlayerSpendingStatus()
    {
        _isPlayerSpending = true;
    }

    private void OnAnimationCompleted()
    {
        if (_isPlayerSpending)
            Destroy(this.gameObject);

        if (_isCollectedByPlayer)
        {
            _player.HubResources.TakeResource(_pack, this.transform);
            Destroy(this.gameObject);
        }

    }

    private void DisablePhisics()
    {
        foreach (var colider in _colliders)
        {
            colider.enabled = false;
        }
        _rigidbody.isKinematic = true;
    }
}