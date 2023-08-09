using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultPointer : MonoBehaviour
{
    [SerializeField] private float _speed;

    private FloatingJoystick _floatingJoystick;
    private CanvasGroup _pointer;
    private CameraCoordinatesGrid _grid;
    private CatapultFireSystem _catapult;
    private PointerStartPosition _basePosition;
    private Coroutine _moving;
    private Coroutine _waitingPointerToStop;
    private bool _isMoving;
    private Timer _timer;
    private float _delayBeforeShoot = 1.5f;
    private bool _isReadyToShoot = true;
    private List<NPC> _npc = new List<NPC>();


    public event Action<Vector3> TargetFound;

    private void Awake()
    {
        _pointer = GetComponentInChildren<CanvasGroup>();
        _grid = GetComponentInChildren<CameraCoordinatesGrid>();
        _timer = GetComponentInChildren<Timer>();
        _catapult = GetComponentInParent<CatapultFireSystem>();
        _basePosition = GetComponentInParent<PointerStartPosition>();
        _isMoving = false;
    }

    private void OnEnable()
    {
        //  _catapult.PlayerStartedAiming += OnPlayerStartedAiming;
    }

    private void OnDisable()
    {
        //  _catapult.PlayerStartedAiming -= OnPlayerStartedAiming;
    }

    public void SetFloatingJoystick(FloatingJoystick joystick)
    {
        _floatingJoystick = joystick;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            if (npc.CharacterType == CharacterType.Enemy)
            {
                _npc.Add(npc);

                if (_waitingPointerToStop == null)
                    _waitingPointerToStop = StartCoroutine(WaitingPointerToStop());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc))
        {
            if (npc.CharacterType == CharacterType.Enemy)
            {
                _npc.Remove(npc);

                if (_npc.Count == 0 && _waitingPointerToStop != null)
                {
                    StopCoroutine(_waitingPointerToStop);
                    _waitingPointerToStop = null;
                    _isReadyToShoot = true;
                }
            }
        }
    }

    private void OnPlayerStartedAiming(CatapultPointer pointer)
    {
        if (_moving == null)
        {
            _isMoving = true;
            _moving = StartCoroutine(Moving());
        }
    }

    private void OnTimerCompleted()
    {
        TargetFound?.Invoke(this.transform.position);

        _isMoving = false;
    }

    private IEnumerator Moving()
    {
        var timeToFade = 1f;

        transform.parent = null;

        _pointer.DOFade(1f, timeToFade);
        Tween delay = _catapult.Model.DOLookAt(this.transform.position, timeToFade);
        yield return delay.WaitForCompletion();

        _timer.Completed += OnTimerCompleted;

        while (_isMoving)
        {
            if (_floatingJoystick.Direction != Vector2.zero)
            {
                _grid.UpdateGrid();

                transform.position += ((_grid.transform.forward * _floatingJoystick.Direction.y) + (_grid.transform.right * _floatingJoystick.Direction.x)) * _speed * Time.deltaTime;

                _catapult.Model.LookAt(this.transform);
            }

            yield return null;
        }

        _timer.Completed -= OnTimerCompleted;

        delay = _pointer.DOFade(0f, timeToFade);
        yield return delay.WaitForCompletion();

        float _flyingTime = 2.5f;

        Tween flying = transform.DOMove(_basePosition.transform.position, _flyingTime);
        yield return flying.WaitForCompletion();

        transform.parent = _basePosition.transform;
        _catapult.Model.DORotate(Vector3.zero, timeToFade);

        _moving = null;
    }

    private IEnumerator WaitingPointerToStop()
    {
        while (_npc.Count > 0)
        {
            if (_floatingJoystick.Direction == Vector2.zero && _isReadyToShoot == true)
            {
                _timer.StartTimer(_delayBeforeShoot);
                _isReadyToShoot = false;
            }
            else if (_floatingJoystick.Direction != Vector2.zero && _isReadyToShoot == false)
            {
                _timer.StopTimer();
                _isReadyToShoot = true;
            }

            yield return null;
        }

        _isReadyToShoot = true;
        _waitingPointerToStop = null;
    }
}
