using _ProjectAssets.Scripts.Pointers;
using System;
using System.Collections;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    [SerializeField] private BattleButton _battleButton;

    [Header("Pointer System")]
    [SerializeField] private PointerManager _pointerManager;
    [SerializeField] private Pointer _pointer;

    private AudioSource _horn;
    private PlayerSquad _playerSquad;
    private EnemyArmyObserver _observer;
    private bool _isReadyToStart = true;
    private Coroutine _waitingToPressAttackButton;
    private Coroutine _waitingToPressSkipButton;
    private KeyboardInput _keyboardInput;

    public bool IsReadyToStart => _isReadyToStart;

    public event Action BattleStarted;

    private void Awake()
    {
        _observer = GetComponentInParent<EnemyArmyObserver>();
        _playerSquad = GetComponentInParent<PlayerSquad>();
        _horn = GetComponent<AudioSource>();

        _keyboardInput = new KeyboardInput();
    }

    private void OnEnable()
    {
        _observer.LevelFinisherReached += OnLevelFinisherRiched;
        _playerSquad.SquadEmpty += OnPlayerSquadEmpty;
        _playerSquad.BattleEnded += OnBattleEnded;
    }
    private void OnDisable()
    {
        _observer.LevelFinisherReached -= OnLevelFinisherRiched;
        _playerSquad.SquadEmpty -= OnPlayerSquadEmpty;
        _playerSquad.BattleEnded -= OnBattleEnded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_waitingToPressAttackButton == null)
                _waitingToPressAttackButton = StartCoroutine(WaitingToStartButtle());

            if (_waitingToPressSkipButton == null)
                _waitingToPressSkipButton = StartCoroutine(WaitingToPressSpace());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_battleButton.gameObject.activeSelf)
            {
                _battleButton.gameObject.SetActive(false);
                _battleButton.Button.onClick.RemoveListener(OnButtonClick);
            }

            if (_waitingToPressAttackButton != null)
            {
                StopCoroutine(_waitingToPressAttackButton);
                _waitingToPressAttackButton = null;
            }

            if (_waitingToPressSkipButton != null)
            {
                StopCoroutine(_waitingToPressSkipButton);
                _waitingToPressSkipButton = null;
            }
        }
    }

    public void EnablePointer()
    {
        _pointerManager.Add(_pointer);
    }

    public void OnButtonClick()
    {
        _isReadyToStart = false;
        _pointerManager.Remove(_pointer);
        BattleStarted?.Invoke();
        _horn.Play();
        _battleButton.gameObject.SetActive(false);
    }

    private void OnPlayerSquadEmpty(Squad squad)
    {
        _isReadyToStart = true;
        _pointerManager.Add(_pointer);
    }

    private void OnBattleEnded()
    {
        _isReadyToStart = false;
        _pointerManager.Remove(_pointer);
    }

    private void OnLevelFinisherRiched(LevelFinisher finisher)
    {
        if (!finisher.IsLastFinisher)
        {
            _isReadyToStart = true;
            _pointerManager.Add(_pointer);
        }
    }

    private IEnumerator WaitingToStartButtle()
    {
        while (true)
        {
            if (_isReadyToStart && _playerSquad.WaitingUnits.Count > 0)
            {
                if (!_battleButton.gameObject.activeSelf)
                {
                    _battleButton.gameObject.SetActive(true);
                    _battleButton.Button.onClick.AddListener(OnButtonClick);
                }
            }
            else
            {
                if (_battleButton.gameObject.activeSelf)
                {
                    _battleButton.gameObject.SetActive(false);
                    _battleButton.Button.onClick.RemoveListener(OnButtonClick);
                }
            }

            yield return null;
        }

        _waitingToPressAttackButton = null;
    }

    private IEnumerator WaitingToPressSpace()
    {
        while (true)
        {
            if (_battleButton.gameObject.activeSelf && _keyboardInput.IsSkipButtonPressed())
            {
                _battleButton.Button.onClick.Invoke();
            }

            yield return null;
        }

        _waitingToPressSkipButton = null;
    }
}
