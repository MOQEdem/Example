using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Agava.YandexMetrica;

public class DragonLair : MonoBehaviour
{
    [SerializeField] private EnemyArmyObserver _observer;
    [SerializeField] private int _cooldown = 10;

    private Dragon _dragon;
    private DragonPointer _pointer;
    private Player _player;
    private PlayerTrigger _trigger;
    private Timer _timer;
    private bool _isReady = true;
    private bool _isHaveTargets = true;
    private Coroutine _waitingToPlayerStop;
    private Coroutine _waitingToPlayerComeDown;
    private DragonADS _dragonADS;
    private Vector3 _playerPosition;

    private BattleStarter _battleStarter;

    public event Action<DragonPointer> DragonLaunched;
    public event Action DragonComeDown;

    private void Awake()
    {
        _timer = GetComponentInChildren<Timer>();
        _trigger = GetComponentInChildren<PlayerTrigger>();
        _dragon = GetComponentInChildren<Dragon>();
        _pointer = GetComponentInChildren<DragonPointer>();
        _dragonADS = GetComponent<DragonADS>();

        _battleStarter = FindObjectOfType<BattleStarter>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
        _timer.Completed += OnTimerCompleted;
        _observer.EnemyArmyDefeted += OnEnemyArmyDefeted;
        _observer.LevelFinisherReached += OnLevelFinisherReached;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
        _timer.Completed += OnTimerCompleted;
        _observer.EnemyArmyDefeted -= OnEnemyArmyDefeted;
        _observer.LevelFinisherReached -= OnLevelFinisherReached;
    }

    private void OnPlayerEnter(Player player)
    {
        if (_player == null)
        {
            _player = player;
            _pointer.SetFloatingJoystick(_player.Joystick);
        }

        if (_waitingToPlayerStop == null && _isHaveTargets)
        {
            _waitingToPlayerStop = StartCoroutine(WaitingToPlayerStop(player));
        }
    }

    private void OnPlayerExit(Player player)
    {
        if (_waitingToPlayerStop != null)
        {
            StopCoroutine(_waitingToPlayerStop);
            _waitingToPlayerStop = null;
        }
    }

    private void OnEnemyArmyDefeted(Transform transform)
    {
        _isHaveTargets = false;
        _dragon.EndFlying();
    }

    private void OnLevelFinisherReached(LevelFinisher finisher)
    {
        _isHaveTargets = true;
    }

    private IEnumerator WaitingToPlayerStop(Player player)
    {
        float jumpHeight = 2f;
        float animationTime = 1f;
        bool isWorking = true;

        while (isWorking)
        {
            if (_isReady && player.PlayerStack.IsReadyToTransit)
            {
                if (!_dragonADS.IsWatched)
                {
                    _dragonADS.TryWatchADS(player.PlayerAccess.IsPlayerHaveAccess(AccessType.Dragon));
                }
                else
                {

#if !UNITY_EDITOR
            YandexMetrica.Send("dragonADS");
#endif

                    _isReady = false;
                    _dragonADS.ResetWatchedStatus();

                    _player.PlayerMover.SetActivity(false);
                    _player.PlayerStack.SetActivity(false);

                    _player.PlayerModel.transform.parent = null;


                    Vector3 startPosition = _player.transform.position;
                    Vector3 endPosition = _dragon.Saddle.transform.position;
                    Vector3 upPosition = startPosition + (endPosition - startPosition) / 2 + (Vector3.up * jumpHeight);

                    _playerPosition = _player.PlayerModel.transform.position;
                    Vector3[] path = new Vector3[] { startPosition, upPosition, endPosition };

                    Tween pathMove = _player.PlayerModel.transform.DOPath(path, animationTime, PathType.CatmullRom);
                    yield return pathMove.WaitForCompletion();

                    if (!_player.PlayerMover.IsMounted)
                        _player.PlayerMover.PlayerAnimator.SetRidingStatus(true);

                    _player.PlayerModel.transform.parent = _dragon.Saddle.transform;
                    _player.PlayerModel.transform.DORotate(Vector3.zero, animationTime);

                    DragonLaunched?.Invoke(_pointer);
                    _battleStarter.OnButtonClick();

                    _dragon.Landed += OnLanded;

                    isWorking = false;
                }
            }

            yield return null;
        }

        _waitingToPlayerStop = null;
    }

    private void OnLanded()
    {
        _dragon.Landed -= OnLanded;

        if (_waitingToPlayerComeDown == null)
            _waitingToPlayerComeDown = StartCoroutine(WaitingToPlayerComeDown());
    }

    private IEnumerator WaitingToPlayerComeDown()
    {
        float jumpHeight = 2f;
        float animationTime = 1f;

        _player.PlayerModel.transform.parent = null;

        _player.PlayerMover.PlayerAnimator.SetRidingStatus(false);

        Vector3 startPosition = _player.PlayerModel.transform.position;
        Vector3 endPosition = _playerPosition;

        Vector3 upPosition = startPosition + (endPosition - startPosition) / 2 + (Vector3.up * jumpHeight);

        Vector3[] path = new Vector3[] { startPosition, upPosition, endPosition };
        Tween pathMove = _player.PlayerModel.transform.DOPath(path, animationTime, PathType.CatmullRom);
        yield return pathMove.WaitForCompletion();

        if (_player.PlayerMover.IsMounted)
            _player.PlayerMover.PlayerAnimator.SetRidingStatus(true);

        _player.PlayerModel.transform.parent = _player.transform;
        _player.PlayerModel.transform.DOLocalRotate(Vector3.zero, animationTime);
        _player.PlayerMover.SetActivity(true);
        _player.PlayerStack.SetActivity(true);

        _timer.StartTimer(_cooldown);
        DragonComeDown?.Invoke();

        _waitingToPlayerComeDown = null;
    }

    private void OnTimerCompleted()
    {
        _isReady = true;
    }
}
