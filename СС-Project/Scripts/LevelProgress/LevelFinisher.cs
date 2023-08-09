using System;
using UnityEngine;

public class LevelFinisher : MonoBehaviour
{
    [Header("Main Options")]
    [SerializeField] private bool _isLastFinisher;
    [SerializeField] private float _timeToCapture;
    [SerializeField] private Animator _flag;
    [Space]
    [Header("Optional Options")]
    [SerializeField] private Transform _activatedObject;
    [SerializeField] private Animator _goldSign;
    [SerializeField] private Tutorial _tutorial;

    private PlayerTrigger _trigger;
    private Timer _timer;

    public bool IsLastFinisher => _isLastFinisher;

    public event Action<LevelFinisher> LevelComplite;

    private void Awake()
    {
        _trigger = GetComponentInChildren<PlayerTrigger>(true);
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
        _timer.Completed += OnTimerComplite;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
        _timer.Completed -= OnTimerComplite;
    }

    public void SetActiveStatus(bool isActive)
    {
        _trigger.gameObject.SetActive(isActive);

        if (_activatedObject != null)
            _activatedObject.gameObject.SetActive(isActive);

        if (_goldSign != null)
            _goldSign.gameObject.SetActive(isActive);
    }

    public void StartStoreTutorial() => _tutorial.StartStoreEducation();
    public void StartChestTutorial() => _tutorial.StartChestEducation();

    private void OnPlayerEnter(Player player)
    {
        _timer.StartTimer(_timeToCapture);

        if (_flag != null)
        {
            _flag.SetBool("IsCaptured", true);
        }
    }

    private void OnPlayerExit(Player player)
    {
        _timer.StopTimer();

        if (_flag != null)
        {
            _flag.SetBool("IsCaptured", false);
        }
    }

    private void OnTimerComplite()
    {
        LevelComplite?.Invoke(this);
    }
}
