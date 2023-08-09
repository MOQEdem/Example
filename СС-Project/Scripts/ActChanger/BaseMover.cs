using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BaseMover : MonoBehaviour
{
    [SerializeField] private BaseMovePoint[] _movePoints;
    [SerializeField] private float _delayBeforeMove;
    [SerializeField] private Transform _pointToMovePlayer;

    private Player _player;
    private Timer _timer;
    private int _currentPoint = -1;
    private PlayerSquad _squad;
    private Storage _storage;

    public event Action BaseMoved;
    public event Action ProgressSaved;

    private void Awake()
    {
        _storage = GetComponentInChildren<Storage>();
        _timer = GetComponent<Timer>();
        _squad = GetComponentInChildren<PlayerSquad>();
    }

    private void OnEnable()
    {
        _timer.Completed += OnTimerComplited;
    }

    private void OnDisable()
    {
        _timer.Completed -= OnTimerComplited;
    }

    public void MoveStorageToTheFinish(Vector3 position)
    {
        StartCoroutine(MovingStorageToTheFinish(position));
    }

    public void MoveBaseNextPoint(PlayableCharacter player)
    {
        _currentPoint++;

        _player = player.GetComponent<Player>();

        _timer.StartTimer(_delayBeforeMove);
    }

    public void SaveProgress()
    {
        ProgressSaved?.Invoke();
    }

    private void OnTimerComplited()
    {
        this.transform.position = _movePoints[_currentPoint].transform.position;

        _player.PlayerMover.SetActivity(false);

        _player.transform.position = _pointToMovePlayer.transform.position;

        _player.PlayerMover.SetActivity(true);
        _squad.ClearSquad();
        _squad.TeleportWaitingSquad();

        BaseMoved?.Invoke();
    }

    private IEnumerator MovingStorageToTheFinish(Vector3 position)
    {
        float animationTime = 1f;

        _storage.transform.parent = null;

        _storage.transform.DORotate(new Vector3(0, 720, 0), animationTime, RotateMode.FastBeyond360);
        Tween tween = _storage.transform.DOScale(0f, animationTime);
        yield return tween.WaitForCompletion();

        _storage.transform.position = position;

        _storage.transform.DOKill();

        _storage.transform.DORotate(new Vector3(0, 540, 0), animationTime, RotateMode.FastBeyond360);
        tween = _storage.transform.DOScale(1f, animationTime);
        yield return tween.WaitForCompletion();
    }
}
