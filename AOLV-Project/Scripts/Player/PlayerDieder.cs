using UnityEngine;
using UnityEngine.AI;

public class PlayerDieder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAttacker _playerAttacker;
    [SerializeField] private RagdollActivator _ragdollActivator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Healer _healer;
    
    private Vector3 _lastAttackPoint;

    private void OnEnable()
    {
        _player.Timer.TimerBrain.Stopped += OnPlayerRespawn;
        _player.Died += OnPlayerDied;
        _player.Hited += OnPlayerHited;
    }

    private void OnDisable()
    {
        _player.Timer.TimerBrain.Stopped -= OnPlayerRespawn;
        _player.Died -= OnPlayerDied;
        _player.Hited -= OnPlayerHited;
    }

    private void OnPlayerHited(AttackData attackData)
    {
        _lastAttackPoint = attackData.AttackPosition;
    }

    private void OnPlayerDied(Unit unit)
    {
        _playerMover.enabled = false;
        _playerAttacker.enabled = false;
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
        _animator.SetBool(AnimatorConst.Died, true);
    }
    
    private void OnPlayerRespawn()
    {
        _playerMover.enabled = true;
        _playerAttacker.enabled = true;
        _agent.isStopped = false;
        _agent.velocity = Vector3.zero;
        _animator.SetBool(AnimatorConst.Died, false);
        _player.Health.IncreaseHealth(_player.Health.MaxHealth);
    }
    
}
