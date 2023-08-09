using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartCameraMover : MonoBehaviour
{
    [Header("Camera Type")]
    [SerializeField] private StartCameraType _type = StartCameraType.JustMove;
    [Space]
    [Header("Camera Settings")]
    [SerializeField] private Button _skipButton;
    [SerializeField] private StartCameraPointToMove[] _path;
    [Space]
    [Header("Additional Objects")]
    [SerializeField] private GameStartBossShower _boss;
    [SerializeField] private float _moveToBossTime;

    private Coroutine _moving;
    private bool _isBossAnimationPlayed = false;

    public event Action MovementComplite;

    private void OnEnable()
    {
        _skipButton.onClick.AddListener(OnSkipButtonClick);

        if (_boss != null)
            _boss.AnimationPlayed += OnBossAnimationPlayed;
    }

    private void OnDisable()
    {
        _skipButton.onClick.RemoveListener(OnSkipButtonClick);

        if (_boss != null)
            _boss.AnimationPlayed -= OnBossAnimationPlayed;
    }

    public void StartToMove(EnemyArmy army)
    {
        if (_type == StartCameraType.JustMove && _path.Length > 0)
            StartRegularMove();
        else if (_type == StartCameraType.ShowBoss && _boss != null)
            StartBossShowMove();
        else
            MovementComplite?.Invoke();
    }

    private void StartRegularMove()
    {
        if (_moving == null)
        {
            _moving = StartCoroutine(Moving());
            StartCoroutine(WaitingToPressSpace());
        }
    }

    private void StartBossShowMove()
    {
        if (_moving == null)
        {
            _moving = StartCoroutine(ShowingBoss());
            StartCoroutine(WaitingToPressSpace());
        }
    }

    private IEnumerator Moving()
    {
        var delay = new WaitForSeconds(1f);

        for (int i = 0; i < _path.Length; i++)
        {
            Tween pathMoveTeween = transform.DOMove(_path[i].Point.position, _path[i].TimeToMove);
            yield return pathMoveTeween.WaitForCompletion();
        }

        MovementComplite?.Invoke();
        _moving = null;
    }

    private IEnumerator ShowingBoss()
    {
        Vector3 startPosition = transform.position;
        var delay = new WaitForSeconds(1f);

        yield return delay;

        Tween patchMoveTeween = transform.DOMove(_boss.transform.position, _moveToBossTime);
        yield return patchMoveTeween.WaitForCompletion();

        _boss.StartPlayAnimation();

        while (!_isBossAnimationPlayed)
        {
            yield return null;
        }

        patchMoveTeween = transform.DOMove(startPosition, _moveToBossTime);
        yield return patchMoveTeween.WaitForCompletion();

        MovementComplite?.Invoke();
        _moving = null;
    }

    private void OnSkipButtonClick()
    {
        if (_moving != null)
            StopCoroutine(_moving);

        if (_boss != null)
            Destroy(_boss.gameObject);

        MovementComplite?.Invoke();

        transform.DOKill();
    }

    private IEnumerator WaitingToPressSpace()
    {
        while (_moving != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _skipButton.onClick.Invoke();
            }

            yield return null;
        }
    }

    private void OnBossAnimationPlayed()
    {
        _isBossAnimationPlayed = true;
    }

    public enum StartCameraType
    {
        JustMove,
        ShowBoss
    }

    [Serializable]
    public class StartCameraPointToMove
    {
        [SerializeField] private Transform _point;
        [SerializeField] private float _timeToMove;

        public Transform Point => _point;
        public float TimeToMove => _timeToMove;
    }
}
