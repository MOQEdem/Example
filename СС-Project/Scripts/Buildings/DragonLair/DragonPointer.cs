using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class DragonPointer : MonoBehaviour
{
    [SerializeField] private float _speed;

    private FloatingJoystick _floatingJoystick;
    private CanvasGroup _pointer;
    private CameraCoordinatesGrid _grid;
    private DragonLair _lair;
    private Transform _basePosition;
    private Coroutine _moving;
    private Coroutine _movingToBase;
    private Coroutine _fixingPosition;
    private Dragon _dragon;
    private bool _isMoving;
    private Timer _timer;
    private float _delayBetweenShoot;
    private bool _isReadyToShoot = true;
    private List<NPC> _npc = new List<NPC>();


    public event Action<Vector3> TargetFound;
    public event Action ArrivedToBase;

    private void Awake()
    {
        _pointer = GetComponentInChildren<CanvasGroup>();
        _grid = GetComponentInChildren<CameraCoordinatesGrid>();
        _timer = GetComponentInChildren<Timer>();
        _dragon = GetComponentInChildren<Dragon>();
        _lair = GetComponentInParent<DragonLair>();
        _basePosition = _lair.transform;
        _isMoving = false;
        _delayBetweenShoot = _dragon.DelayBetweenShoot;
    }

    private void OnEnable()
    {
        _dragon.TakeWing += OnTakeWing;
        _dragon.FlyingEnd += OnFlyingEnd;
        _timer.Completed += OnTimerCompleted;
    }

    private void OnDisable()
    {
        _dragon.TakeWing -= OnTakeWing;
        _dragon.FlyingEnd -= OnFlyingEnd;
        _timer.Completed -= OnTimerCompleted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            if (npc.CharacterType == CharacterType.Enemy)
            {
                _npc.Add(npc);
            }
        }

        if (other.TryGetComponent<LevelWall>(out LevelWall levelWall))
        {
            FixPosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            if (npc.CharacterType == CharacterType.Enemy)
            {
                _npc.Remove(npc);
            }
        }
    }

    public void FixPosition()
    {
        if (_fixingPosition == null)
            _fixingPosition = StartCoroutine(FixingPosition());
    }

    public void SetFloatingJoystick(FloatingJoystick joystick)
    {
        _floatingJoystick = joystick;
    }

    private void TryToShoot()
    {
        if (_npc.Count > 0 && _isReadyToShoot)
        {
            _isReadyToShoot = false;
            TargetFound?.Invoke(this.transform.position);
            _timer.StartTimer(_delayBetweenShoot);
        }
    }

    private void OnTimerCompleted()
    {
        _timer.StopTimer();
        _isReadyToShoot = true;
    }

    private void OnTakeWing()
    {
        if (_moving == null)
        {
            _isMoving = true;
            _moving = StartCoroutine(Moving());
        }
    }

    private void OnFlyingEnd()
    {
        _isMoving = false;

        if (_moving != null)
        {
            StopCoroutine(_moving);
            var timeToFade = 0.5f;
            _pointer.DOFade(0f, timeToFade);
            _moving = null;
        }

        if (_movingToBase == null)
            _movingToBase = StartCoroutine(MovingToBase());
    }

    private IEnumerator Moving()
    {
        var timeToFade = 0.5f;

        _pointer.DOFade(1f, timeToFade);

        transform.parent = null;

        while (_isMoving)
        {
            if (_fixingPosition == null)
            {
                if (_floatingJoystick.Direction != Vector2.zero)
                {
                    _grid.UpdateGrid();

                    transform.position += ((_grid.transform.forward * _floatingJoystick.Direction.y) + (_grid.transform.right * _floatingJoystick.Direction.x)) * _speed * Time.deltaTime;

                    _dragon.transform.rotation = Quaternion.LookRotation(new Vector3(_floatingJoystick.Direction.x, 0f, _floatingJoystick.Direction.y), Vector3.up) * Quaternion.Euler(0, _grid.transform.rotation.eulerAngles.y, 0);
                }

                TryToShoot();
            }

            yield return null;
        }

        _pointer.DOFade(0f, timeToFade);
        _moving = null;
    }

    private IEnumerator MovingToBase()
    {
        float _flyingTime = 2.5f;
        float _rotationTime = 1f;

        Tween flying = transform.DOLookAt(_basePosition.position, _rotationTime);
        yield return flying.WaitForCompletion();


        flying = transform.DOMove(_basePosition.position, _flyingTime);
        yield return flying.WaitForCompletion();

        ArrivedToBase?.Invoke();
        transform.parent = _lair.transform;

        this.transform.DOLocalRotate(Vector3.zero, _rotationTime);
        _npc.Clear();

        _movingToBase = null;
    }

    private IEnumerator FixingPosition()
    {
        float fixTime = 1.5f;
        float fixSpeed = 2f;

        while (fixTime > 0)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, _basePosition.position, fixSpeed * Time.deltaTime);

            fixTime -= Time.deltaTime;
            yield return null;
        }

        _fixingPosition = null;
    }
}
