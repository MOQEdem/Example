using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMover : MonoBehaviour
{
    [Header("Links")] [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;

    [Header("MovementParameters")] [SerializeField]
    private float _startMovementSpeed = 1;

    [SerializeField, Range(0, 1)] private float _reducedSpeedCoefficient = 0.5f;

    [Header("SurfaceCheking")] [SerializeField]
    private TargetRayCastChecker _groundChecker;

    [SerializeField] private WaterChecker _waterChecker;
    [SerializeField] private KeyboardInput _keyboardInput;

    private NavMeshAgent _meshAgent;
    private Rigidbody _rigidbody;

    private Camera _camera;
    private float _movementSpeed;
    private float _inputDeflection;
    private bool _isActive = true;
    private MovementMode _movementMode = MovementMode.NavMesh;
    private Vector2 _inputDirection;

    private void Awake()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _movementSpeed = _startMovementSpeed;
    }

    private void FixedUpdate()
    {
        if (!_isActive)
            return;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _inputDirection = _keyboardInput.SetInputDirection();
            _inputDeflection = 1;
            Move();
            return;
        }
        if (_joystick.Direction != Vector2.zero)
        {
            _inputDirection = _joystick.Direction;
            _inputDeflection = _joystick.Direction.magnitude;
            Move();
            return;
        }

        _inputDirection = Vector2.zero;
        Move();
    }

    private void Move()
    {
        if (_inputDirection == Vector2.zero)
        {
            TryMove(Vector3.zero);
        }
        else
        {
            transform.rotation =
                Quaternion.LookRotation(new Vector3(_inputDirection.x, 0f, _inputDirection.y), Vector3.up) *
                Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
            // SelectMovemenMode();

            TryMove(transform.forward * _inputDeflection);
            _inputDeflection = 0;
        }
    }

    public void Enable()
    {
        _isActive = true;
    }

    public void Disable()
    {
        _isActive = false;
    }

    public void SetReducedSpeed()
    {
        _movementSpeed = _startMovementSpeed * _reducedSpeedCoefficient;
    }

    public void SetDefaultSpeed()
    {
        _movementSpeed = _startMovementSpeed;
    }

    private void SelectMovemenMode()
    {
        bool isGrounded = _groundChecker.TryCheckTarget();
        if (isGrounded == true && _movementMode == MovementMode.Phisics)
        {
            _groundChecker.SetMaxChekingRadius();
            SetMovementMode(MovementMode.NavMesh);
        }
        else if (isGrounded == false && _movementMode == MovementMode.NavMesh)
        {
            _groundChecker.SetMinChekingRadius();
            SetMovementMode(MovementMode.Phisics);
        }
    }

    private void SetMovementMode(MovementMode mode)
    {
        bool isNavMeshActive = mode == MovementMode.NavMesh;
        _rigidbody.isKinematic = isNavMeshActive;
        _meshAgent.enabled = isNavMeshActive;
        SetZeroVelocity();
        _movementMode = mode;
    }

    private void SetZeroVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _meshAgent.velocity = Vector3.zero;
    }

    private void TryMove(Vector3 direction)
    {
        SetAnimatorState();

        if (_movementMode == MovementMode.NavMesh)
            _meshAgent.velocity = direction * _movementSpeed;
        else if (_movementMode == MovementMode.Phisics)
            _rigidbody.MovePosition(_rigidbody.position + direction * _movementSpeed * Time.fixedDeltaTime);
    }

    private void SetAnimatorState()
    {
        _animator.SetBool(AnimatorConst.IsSwimp, _waterChecker.IsWaterDetected);
        _animator.SetFloat(AnimatorConst.Speed, _inputDeflection);
    }

    private enum MovementMode
    {
        Phisics,
        NavMesh
    }
}