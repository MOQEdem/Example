using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartCameraView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameUI;
    [SerializeField] private Button _skipButton;
    [SerializeField] private string _levelName;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private int _levelOfGroupNumber;
    [SerializeField] private StartCameraMover _mover;
    [SerializeField] private DialoguePlayer _dialoguePlayer;

    private GlobalProgressBar _bar;
    private GameProgress _progress;

    public string LevelName => _levelName;

    private void Awake()
    {
        _bar = GetComponentInChildren<GlobalProgressBar>();
        _bar.SetHeaderText(_levelName);
        _bar.SetCurrentView(_levelOfGroupNumber);

        _progress = new GameProgress();
        _progress.Load();
    }

    private void OnEnable()
    {
        _skipButton.onClick.AddListener(OnSkipButtonClick);
        _mover.MovementComplite += OnSkipButtonClick;
    }

    private void OnDisable()
    {
        _skipButton.onClick.RemoveListener(OnSkipButtonClick);
        _mover.MovementComplite -= OnSkipButtonClick;
    }

    private void Start()
    {
        _gameUI.alpha = 0f;
        _levelNumberText.text = _progress.CurrentLevel.ToString();
    }

    private void OnSkipButtonClick()
    {
        this.gameObject.SetActive(false);

        if (_dialoguePlayer != null)
            _dialoguePlayer.StartDialogue();
        else
            _gameUI.alpha = 1f;
    }
}
