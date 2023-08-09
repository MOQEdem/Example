using UnityEngine;

public class StorageAnimator : MonoBehaviour
{
    [SerializeField] private PlayerTrigger[] _trigger;

    private Animator _animator;

    private const string _open = "Open";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        foreach (var trigger in _trigger)
        {
            trigger.Enter += OnPlayerEnter;
            trigger.Exit += OnPlayerExit;
        }
    }

    private void OnDisable()
    {
        foreach (var trigger in _trigger)
        {
            trigger.Enter -= OnPlayerEnter;
            trigger.Exit -= OnPlayerExit;
        }
    }

    private void OnPlayerEnter(Player player)
    {
        _animator.SetBool(_open, true);
    }

    private void OnPlayerExit(Player player)
    {
        _animator.SetBool(_open, false);
    }
}
