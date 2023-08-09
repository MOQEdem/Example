using UnityEngine;
using DG.Tweening;
using Agava.YandexMetrica;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField] private EnemyArmyController _enemyArmyController;
    [SerializeField] private PlayableCharacter _player;
    [SerializeField] private LevelWinCanvas _winCanvas;
    [SerializeField] private CanvasGroup _mainCanvas;
    [SerializeField] private PlayerDefetedCanvas _loseCanvas;
    [SerializeField] private BaseMover _baseMover;
    [SerializeField] private DialoguePlayer _endDialogue;

    private CanvasGroup _winCanvasGroup;
    private CanvasGroup _loseCanvasGroup;
    private TargetIndicator _targetIndicator;
    private StoreActivator _storeActivator;

    private void Awake()
    {
        _winCanvasGroup = _winCanvas.GetComponent<CanvasGroup>();
        _loseCanvasGroup = _loseCanvas.GetComponent<CanvasGroup>();

        _targetIndicator = GetComponentInChildren<TargetIndicator>();
        _targetIndicator.gameObject.SetActive(false);

        _storeActivator = GetComponentInChildren<StoreActivator>(true);

        SetCanvasStatus(_winCanvasGroup, false);
        SetCanvasStatus(_loseCanvasGroup, false);
    }

    private void OnEnable()
    {
        _enemyArmyController.ArmyDefeted += OnArmyDefeted;
        _enemyArmyController.LevelFinisherReached += OnLevelComplite;
        _player.Died += OnPlayerDied;
        _player.Resurrected += OnPlayerResurrected;
    }

    private void OnDisable()
    {
        _enemyArmyController.ArmyDefeted -= OnArmyDefeted;
        _enemyArmyController.LevelFinisherReached -= OnLevelComplite;
        _player.Died -= OnPlayerDied;
        _player.Resurrected -= OnPlayerResurrected;
    }

    private void OnLevelComplite(LevelFinisher finisher)
    {
        finisher.LevelComplite -= OnLevelComplite;

        _baseMover.SaveProgress();

        if (finisher.IsLastFinisher)
        {
            _player.GetComponent<PlayerMover>().SetActivity(false);

            if (_endDialogue.IsHaveMessages)
                _endDialogue.StartDialogue();

            SetCanvasStatus(_mainCanvas, false);
            SetCanvasStatus(_winCanvasGroup, true);
        }
        else
        {
            _baseMover.MoveBaseNextPoint(_player);
            _targetIndicator.gameObject.transform.SetParent(null);
            _targetIndicator.gameObject.SetActive(false);
        }
    }

    private void OnArmyDefeted(EnemyArmy army)
    {
        if (army.LevelFinisher.IsLastFinisher == true)
        {
            _baseMover.MoveStorageToTheFinish(army.LevelFinisher.transform.position + (Vector3.left * 8f));

            if (_storeActivator.gameObject.activeSelf)
                _storeActivator.Open(army);
        }

        _targetIndicator.SetPoints(_player.transform, army.LevelFinisher.transform);
        _targetIndicator.gameObject.transform.SetParent(_player.gameObject.transform);
        _targetIndicator.gameObject.transform.localPosition = Vector3.up * 0.3f;
        _targetIndicator.gameObject.SetActive(true);
    }

    private void OnPlayerDied(Character player)
    {
        SetCanvasStatus(_mainCanvas, false);
        SetCanvasStatus(_loseCanvasGroup, true);

#if !UNITY_EDITOR
        YandexMetrica.Send("playerDied");
#endif
    }

    private void OnPlayerResurrected()
    {
        SetCanvasStatus(_mainCanvas, true);
        SetCanvasStatus(_loseCanvasGroup, false);


#if !UNITY_EDITOR
        YandexMetrica.Send("ADSWatched");
#endif
    }

    private void SetCanvasStatus(CanvasGroup canvas, bool isActive)
    {
        float alfa = isActive ? 1f : 0f;
        float fadeTime = isActive ? 0.5f : 0f;

        canvas.DOFade(alfa, fadeTime);
        canvas.blocksRaycasts = isActive;
        canvas.interactable = isActive;
    }
}
