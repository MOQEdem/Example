using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{
    [Header("Dragon Settings")]
    [SerializeField] private int _damage;
    [SerializeField] private int _numberOfFireBalls;
    [SerializeField] private float _delayBetweenShoot = 2f;
    [Space]
    [Header("Fireball Prefabe")]
    [SerializeField] private DragonFireBall _fireBall;
    [Space]
    [Header("Fly Way")]
    [SerializeField] private Transform[] _flyingPoints;
    [Space]
    [Header("End Fly Button")]
    [SerializeField] private Button _endFly;

    private DragonSoundPlayer _soundPlayer;
    private DragonSaddle _saddle;
    private DragonLair _lair;
    private DragonPointer _pointer;
    private DragonFirePoint _firePoint;
    private bool _isFlying = false;
    private Coroutine _flying;
    private Coroutine _flyingDown;
    private Coroutine _waitingToPressSpace;
    private int _currentNumberOfFireBalls;
    private Animator _animator;

    public DragonSaddle Saddle => _saddle;
    public int CurrentNumberOfFireBalls => _currentNumberOfFireBalls;
    public float DelayBetweenShoot => _delayBetweenShoot;

    public event Action TakeWing;
    public event Action FlyingEnd;
    public event Action Landed;
    public event Action NumberOfFireBallsChanged;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _saddle = GetComponentInChildren<DragonSaddle>();
        _firePoint = GetComponentInChildren<DragonFirePoint>();
        _lair = GetComponentInParent<DragonLair>();
        _pointer = GetComponentInParent<DragonPointer>();
        _soundPlayer = GetComponentInChildren<DragonSoundPlayer>();
    }

    private void OnEnable()
    {
        _lair.DragonLaunched += OnDragonLauched;
        _pointer.TargetFound += OnTargetFound;
        _pointer.ArrivedToBase += OnArrivedToBase;
    }

    private void OnDisable()
    {
        _lair.DragonLaunched -= OnDragonLauched;
        _pointer.TargetFound -= OnTargetFound;
        _pointer.ArrivedToBase -= OnArrivedToBase;
    }

    public void EndFlying()
    {
        _endFly.onClick.RemoveListener(EndFlying);

        if (_isFlying)
        {
            _currentNumberOfFireBalls = 0;
            _isFlying = false;
            FlyingEnd?.Invoke();
        }
    }

    private void OnDragonLauched(DragonPointer pointer)
    {
        _isFlying = true;

        if (_flying == null)
        {
            if (_waitingToPressSpace == null)
                _waitingToPressSpace = StartCoroutine(WaitingToPressSpace());

            _flying = StartCoroutine(Flying());
            _currentNumberOfFireBalls = _numberOfFireBalls;
            NumberOfFireBallsChanged?.Invoke();

            _soundPlayer.Jump(true);
        }
    }

    private void OnTargetFound(Vector3 targetPosition)
    {
        if (_currentNumberOfFireBalls > 0)
        {
            _currentNumberOfFireBalls--;
            NumberOfFireBallsChanged?.Invoke();

            if (_currentNumberOfFireBalls == 0)
            {
                _endFly.onClick.RemoveListener(EndFlying);
                _isFlying = false;
                FlyingEnd?.Invoke();
            }

            _soundPlayer.Attack();
            var fireBall = Instantiate(_fireBall, _firePoint.transform);
            fireBall.FlyToEnemy(targetPosition, _damage);
        }
    }

    private void OnArrivedToBase()
    {
        _isFlying = false;

        if (_flying != null)
        {
            if (_waitingToPressSpace != null)
                StopCoroutine(_waitingToPressSpace);

            StopCoroutine(_flying);
            _flying = null;
            this.transform.DOKill();
            _soundPlayer.Run(false);
        }

        _soundPlayer.Jump(false);

        if (_flyingDown == null)
            _flyingDown = StartCoroutine(FlyingDown());

    }

    private IEnumerator Flying()
    {
        _animator.SetTrigger("Up");
        float flyingHeight = 6f;
        float flyingUpAndDownTime = 1.5f;

        Tween flyingUp = this.transform.DOLocalMove(Vector3.up * flyingHeight, flyingUpAndDownTime);
        yield return flyingUp.WaitForCompletion();

        TakeWing?.Invoke();

        _soundPlayer.Run(true);
        Vector3[] flyingPath = new Vector3[_flyingPoints.Length];

        for (int i = 0; i < _flyingPoints.Length; i++)
        {
            flyingPath[i] = _flyingPoints[i].localPosition;
        }

        _endFly.onClick.AddListener(EndFlying);

        float timeToFlyOneLoop = 6f;

        while (_isFlying)
        {
            Tween flyingLoop = transform.DOLocalPath(flyingPath, timeToFlyOneLoop, PathType.CatmullRom).SetLoops(-1).SetOptions(true);
            yield return flyingLoop.WaitForCompletion();
        }

        _flying = null;
    }

    private IEnumerator FlyingDown()
    {
        _animator.SetTrigger("Down");
        float flyingUpAndDownTime = 1.5f;


        Tween flyingDown = this.transform.DOLocalMove(Vector3.zero, flyingUpAndDownTime);
        yield return flyingDown.WaitForCompletion();

        this.transform.DOLocalRotate(Vector3.zero, flyingUpAndDownTime);

        Landed?.Invoke();
        _flyingDown = null;
    }

    private IEnumerator WaitingToPressSpace()
    {
        KeyboardInput keyboardInput = new KeyboardInput();

        while (_isFlying)
        {
            if (keyboardInput.IsSkipButtonPressed())
            {
                _endFly.onClick.Invoke();
            }

            yield return null;
        }

        _waitingToPressSpace = null;
    }
}
