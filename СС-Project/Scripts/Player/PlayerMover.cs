using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _floatingJoystick;
    [SerializeField] private float _speed;


    private CharacterAnimator _playerAnimator;
    private Camera _camera;
    private NavMeshAgent _navMeshAgent;
    private bool _isActive = true;
    private CharacterAudio _audio;
    private KeyboardInput _keyboardInput;
    private float _defaultSpeed;
    private Vector2 _inputDirection;
    private PlayerDash _dash;

    public float Speed => _speed;
    public FloatingJoystick Joystick => _floatingJoystick;
    public CharacterAnimator PlayerAnimator => _playerAnimator;
    public Vector2 InputDirection => _inputDirection;
    public bool IsMounted { get; private set; }

    private void Start()
    {
        _keyboardInput = new KeyboardInput();
        _playerAnimator = GetComponent<CharacterAnimator>();
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _dash = GetComponent<PlayerDash>();
        _audio = GetComponentInChildren<CharacterAudio>();
        IsMounted = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_isActive)
        {
            if (_floatingJoystick.Direction != Vector2.zero)
            {
                _inputDirection = _floatingJoystick.Direction;
            }
            else if (_keyboardInput.SetInputDirection() != Vector2.zero)
            {
                _inputDirection = _keyboardInput.SetInputDirection();
            }
            else
            {
                _inputDirection = Vector2.zero;
            }

            if (_dash.IsAbleToDash)
            {
                if (_keyboardInput.IsDashButtonPressed())
                {
                    _dash.StartCoolDownDash();
                }
            }

            if (_inputDirection != Vector2.zero)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(_inputDirection.x, 0f, _inputDirection.y), Vector3.up) * Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);

                _navMeshAgent.velocity = transform.forward * (_speed + _dash.GetDashPower());
            }
            else if (_dash.IsDashTimeLast)
            {
                _navMeshAgent.velocity = transform.forward * (_speed + _dash.GetDashPower());
            }
            else
            {
                _navMeshAgent.velocity = Vector3.zero;
            }
        }


        if (_inputDirection != Vector2.zero && _isActive)
        {
            _playerAnimator.Run(true);
            _audio.Run(true);
        }
        else
        {
            if (!_dash.IsDashTimeLast)
                _navMeshAgent.velocity = Vector3.zero;

            _playerAnimator.Run(false);
            _audio.Run(false);
        }
    }

    public void SetActivity(bool isActive)
    {
        _isActive = isActive;
        _navMeshAgent.enabled = isActive;

        if (!isActive)
            _playerAnimator.Run(false);
    }

    public void SetNewSpeed(float newSpeed)
    {
        _speed = newSpeed;
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
    }

    public void SetMountedStatus()
    {
        IsMounted = true;
    }

    public void SetWaterSpeed(float newSpeed)
    {
        _defaultSpeed = _speed;
        _speed = newSpeed;
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
    }

    public void SetDefaultSpeed()
    {
        _speed = _defaultSpeed;
    }
}
