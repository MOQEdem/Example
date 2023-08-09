using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Ballista : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private BattleStarter _battleStarter;
    [SerializeField] private PlayerSquad _playerSquad;
    [SerializeField] private EnemyArmyObserver _observer;
    [Space]
    [Header("Ballista Settings")]
    [SerializeField] private BallistaProjectile _ballistaProjectilePrefab;
    [SerializeField] private float _reloadTime;
    [Space]
    [Header("Animation Settings")]
    [SerializeField] private BallistaAnimator _ballistaAnimator;
    [SerializeField] private Transform _projectileShotPoint;

    private Transform _targetPoint;
    private BallistaProjectile _currentProjectile;
    private Timer _timer;
    private bool _isReady = true;
    private bool _isHaveTargets = true;
    private bool _isReloading = false;
    private Coroutine _shooting;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _playerSquad.SquadEmpty += OnSquadEmpty;
        _battleStarter.BattleStarted += OnBattleStarted;
        _timer.Completed += OnTimerCompleted;
        _observer.EnemyArmyDefeted += OnEnemyArmyDefeted;
        _observer.LevelFinisherReached += OnLevelFinisherReached;
    }

    private void OnDisable()
    {
        _playerSquad.SquadEmpty -= OnSquadEmpty;
        _battleStarter.BattleStarted -= OnBattleStarted;
        _timer.Completed -= OnTimerCompleted;
        _observer.EnemyArmyDefeted -= OnEnemyArmyDefeted;
        _observer.LevelFinisherReached -= OnLevelFinisherReached;
    }

    private IEnumerator Shooting()
    {
        while (_isHaveTargets && _isReady)
        {
            if (!_isReloading)
            {
                EnemyArmy army = _observer.CurrentEnemyArmy;
                EnemySquad squad = army.Army[Random.Range(0, _observer.CurrentEnemyArmy.Army.Count)];
                Character npc = squad.Units[Random.Range(0, squad.Units.Count)];

                _targetPoint = npc.transform;

                if (_targetPoint)
                {
                    Tween look = transform.DOLookAt(_targetPoint.position, 0.3f);
                    yield return look.WaitForCompletion();
                }

                _currentProjectile = Instantiate(_ballistaProjectilePrefab, _ballistaProjectilePrefab.transform);
                _currentProjectile.transform.parent = null;
                _ballistaProjectilePrefab.gameObject.SetActive(false);
                _ballistaAnimator.ShowShotAnimation();

                _currentProjectile.MoveTo(_targetPoint);
                yield return new WaitForSeconds(_ballistaAnimator.AnimationTime);

                _isReloading = true;
                _timer.StartTimer(_reloadTime);

                _ballistaAnimator.ShowChargeAnimation();
                yield return new WaitForSeconds(_ballistaAnimator.AnimationTime);
            }

            yield return null;
        }

        transform.DORotate(Vector3.zero, 0.3f);
        _shooting = null;
    }

    private void OnBattleStarted()
    {
        if (_shooting == null)
        {
            _isReady = true;
            _shooting = StartCoroutine(Shooting());
        }
    }

    private void OnSquadEmpty(Squad squad)
    {
        _isReady = false;
    }

    private void OnEnemyArmyDefeted(Transform transform)
    {
        _isHaveTargets = false;
    }

    private void OnLevelFinisherReached(LevelFinisher finisher)
    {
        _isHaveTargets = true;
    }

    private void OnTimerCompleted()
    {
        _isReloading = false;
        _ballistaProjectilePrefab.gameObject.SetActive(true);
    }
}