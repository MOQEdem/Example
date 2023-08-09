using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent), typeof(RagdollActivator))]
public class CapturedState : State
{
    [SerializeField] private Animator _animator;

    private RagdollActivator _ragdollActivator;
    private Enemy _enemy;
    private NavMeshAgent _navMeshAgent;
    private Coroutine _waitEnemyDeath;

    private void Awake()
    {
        _ragdollActivator = GetComponent<RagdollActivator>();
        _enemy = GetComponent<Enemy>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Capture();
    }

    private void Capture()
    {
        if (_navMeshAgent.isActiveAndEnabled == true)
            _navMeshAgent.SetDestination(transform.position);
        _animator.StopPlayback();
        
        _navMeshAgent.enabled = false;
        _enemy.GetRigidbody().isKinematic = false;

        if (_enemy.TryGetComponent(out RagdollActivator ragdollActivator))
        {
            ragdollActivator.SetKinenaticState(false);
        }
        StartCoroutine(WaitEnemyDeath());
    }

    private IEnumerator WaitEnemyDeath()
    {
        float delay = 2f; 
        yield return new WaitForSeconds(delay);
        _enemy.Kill();
    }

    private void OnDisable()
    {
        _navMeshAgent.enabled = true;
        _enemy.GetRigidbody().isKinematic = true;
        _animator.StopPlayback();
        if (_enemy.TryGetComponent(out RagdollActivator ragdollActivator))
        {
            ragdollActivator.SetKinenaticState(true);
        }
    }
}