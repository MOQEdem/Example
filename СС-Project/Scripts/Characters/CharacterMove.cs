using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private NavMeshAgent _navMeshAgent;
    private bool _isMove;
    private CharacterAnimator _animator;
    private Vector3 _target;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public float Speed => _speed;
    public bool IsMove => _isMove;

    public UnityAction MoveEnded;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
        _animator = GetComponent<CharacterAnimator>();
    }

    public void CheckEndPath()
    {
        if (_target != null)
        {
            if (_navMeshAgent.enabled)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    _animator.Run(false);
                    _isMove = false;
                    _navMeshAgent.avoidancePriority = 90;
                }
                else
                {
                    _animator.Run(true);
                    _isMove = true;
                }
            }
        }
    }

    public void ChangeMoveState(bool flag)
    {
        _isMove = flag;
    }

    public void SetSpeedMove(float speed)
    {
        if (_navMeshAgent.enabled)
            _navMeshAgent.speed = speed;
    }

    public void SetGoalToMove(Vector3 target)
    {
        _target = target;
        _navMeshAgent.SetDestination(target);
        _isMove = true;
        _navMeshAgent.avoidancePriority = 40;
    }
}