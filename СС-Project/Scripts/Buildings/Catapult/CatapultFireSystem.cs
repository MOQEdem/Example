using System;
using System.Collections;
using UnityEngine;
using Agava.YandexMetrica;

public class CatapultFireSystem : MonoBehaviour
{
    [Header("Target Source")]
    [SerializeField] private EnemyArmyObserver _observer;
    [Space]
    [Header("Catapult Settings")]
    [SerializeField] private int _cooldown = 10;
    [SerializeField] private int _damage;
    [Space]
    [Header("Animation Model")]
    [SerializeField] private Transform _model;

    private AudioSource _fireSound;
    private Player _player;
    private Animator _animator;
    private TNT _tnt;
    private PlayerTrigger _trigger;
    private Timer _timer;
    private bool _isReady = true;
    private bool _isHaveTargets = true;
    private Coroutine _waitingToPlayerStop;
    private Coroutine _firing;
    private CatapultADS _catapultADS;

    public Transform Model => _model;

    public event Action<TNT> TNTLaunched;

    private void Awake()
    {
        _catapultADS = GetComponent<CatapultADS>();
        _fireSound = GetComponent<AudioSource>();
        _timer = GetComponentInChildren<Timer>();
        _tnt = GetComponentInChildren<TNT>();
        _trigger = GetComponentInChildren<PlayerTrigger>();
        _animator = GetComponentInChildren<Animator>();
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
    }

    private void OnLevelFinisherReached(LevelFinisher finisher)
    {
        _isHaveTargets = true;
    }

    private IEnumerator WaitingToPlayerStop(Player player)
    {

        bool isWorking = true;

        while (isWorking)
        {
            if (_isReady && player.PlayerStack.IsReadyToTransit)
            {
                if (!_catapultADS.IsWatched)
                {
                    _catapultADS.TryWatchADS(player.PlayerAccess.IsPlayerHaveAccess(AccessType.TNT));
                }
                else
                {
#if !UNITY_EDITOR
            YandexMetrica.Send("catapultADS");
#endif

                    EnemyArmy army = _observer.CurrentEnemyArmy;
                    EnemySquad squad = army.Army[UnityEngine.Random.Range(0, _observer.CurrentEnemyArmy.Army.Count)];
                    Character npc = squad.Units[UnityEngine.Random.Range(0, squad.Units.Count)];

                    if (_firing == null)
                        _firing = StartCoroutine(Firing(npc.transform.position));

                    _isReady = false;
                    _player.PlayerMover.SetActivity(false);

                    _catapultADS.ResetWatchedStatus();
                    isWorking = false;
                }
            }

            yield return null;
        }

        _waitingToPlayerStop = null;
    }

    private IEnumerator Firing(Vector3 point)
    {
        TNT currentTNT = Instantiate(_tnt, _tnt.transform);
        currentTNT.Exploded += OnTNTExploded;
        TNTLaunched?.Invoke(currentTNT);

        var delay = new WaitForSeconds(0.25f);
        yield return delay;

        _animator.SetTrigger("Fire");
        _fireSound.Play();

        delay = new WaitForSeconds(0.5f);
        yield return delay;

        currentTNT.Launch(point, _damage);

        _tnt.gameObject.SetActive(false);

        _firing = null;
    }

    private void OnTNTExploded(TNT tnt)
    {
        _player.PlayerMover.SetActivity(true);
        _timer.StartTimer(_cooldown);

        if (_waitingToPlayerStop != null)
        {
            StopCoroutine(_waitingToPlayerStop);
            _waitingToPlayerStop = null;
        }
    }

    private void OnTimerCompleted()
    {
        _isReady = true;
        _tnt.gameObject.SetActive(true);
    }
}
