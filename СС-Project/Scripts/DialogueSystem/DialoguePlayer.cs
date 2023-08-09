using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainUI;
    [SerializeField] private Button _skipMessage;
    [SerializeField] private AudioSource[] _writingSounds;
    [SerializeField] private TMP_Text _speakerName;
    [SerializeField] private Image _leftSpeakerImage;
    [SerializeField] private Image _rightSpeakerImage;
    [SerializeField] private TutorialPointer _tutorial;

    private GameProgress _progress;
    private CanvasGroup _dialogueUI;
    private DialogueMessage[] _messages;
    private int _currentMessageNumber = 0;
    private Coroutine _playingDialogue;
    private Coroutine _skipingAllDialogue;
    private bool _isSkipButtonClick = false;

    public bool IsHaveMessages => _messages.Length > 0;

    private void Awake()
    {
        _messages = GetComponentsInChildren<DialogueMessage>(true);
        _dialogueUI = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _skipMessage.onClick.AddListener(SkipMessage);
    }

    private void OnDisable()
    {
        _skipMessage.onClick.RemoveListener(SkipMessage);
    }

    private void Start()
    {
        foreach (var message in _messages)
            message.gameObject.SetActive(false);

        _progress = new GameProgress();
        _progress.Load();

        if (_progress.CurrentLevel == 1)
            _tutorial.gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        if (_playingDialogue == null)
            _playingDialogue = StartCoroutine(PlayingDialogue());
    }

    private void SkipMessage()
    {
        _isSkipButtonClick = true;
    }

    private void SkipAll()
    {
        if (_playingDialogue != null)
        {
            StopCoroutine(_playingDialogue);
        }

        if (_skipingAllDialogue == null)
        {
            _skipingAllDialogue = StartCoroutine(SkippingDialogue());
        }
    }

    private IEnumerator PlayingDialogue()
    {
        float animationTime = 0.5f;
        Tween tween;

        _mainUI.interactable = false;
        _mainUI.blocksRaycasts = false;
        _mainUI.DOFade(0f, animationTime);

        _dialogueUI.interactable = true;
        _dialogueUI.blocksRaycasts = true;

        float printTime = 1.5f;

        while (_currentMessageNumber < _messages.Length)
        {
            int randomSoundIndex = Random.Range(0, _writingSounds.Length);
            _writingSounds[randomSoundIndex].Play();

            _messages[_currentMessageNumber].gameObject.SetActive(true);

            string message = _messages[_currentMessageNumber].Text.text;

            if (_messages[_currentMessageNumber].IsPlayerCharacterMessage)
            {
                _leftSpeakerImage.sprite = _messages[_currentMessageNumber].SpeakerIcon;
                _leftSpeakerImage.gameObject.SetActive(true);
                _rightSpeakerImage.gameObject.SetActive(false);
            }
            else
            {
                _rightSpeakerImage.sprite = _messages[_currentMessageNumber].SpeakerIcon;
                _rightSpeakerImage.gameObject.SetActive(true);
                _leftSpeakerImage.gameObject.SetActive(false);
            }

            _messages[_currentMessageNumber].Text.text = null;

            if (_dialogueUI.alpha != 1)
            {
                tween = _dialogueUI.DOFade(1f, animationTime);
                yield return tween.WaitForCompletion();
            }


            _messages[_currentMessageNumber].Text.DOText(message, printTime);

            float timeToWrightMessage = printTime;

            while (!_isSkipButtonClick && timeToWrightMessage > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    _skipMessage.onClick.Invoke();

                // timeToWrightMessage -= Time.deltaTime;
                yield return null;
            }

            _messages[_currentMessageNumber].Text.DOKill();
            _messages[_currentMessageNumber].Text.text = message;

            _isSkipButtonClick = false;

            float timeToSwitchMessage = printTime;

            while (!_isSkipButtonClick && timeToSwitchMessage > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    _skipMessage.onClick.Invoke();

                // timeToSwitchMessage -= Time.deltaTime;
                yield return null;
            }

            _messages[_currentMessageNumber].gameObject.SetActive(false);
            _isSkipButtonClick = false;
            _currentMessageNumber++;

            yield return null;
        }

        _dialogueUI.interactable = false;
        _dialogueUI.blocksRaycasts = false;
        tween = _dialogueUI.DOFade(0f, animationTime);
        yield return tween.WaitForCompletion();

        _mainUI.interactable = true;
        _mainUI.blocksRaycasts = true;
        tween = _mainUI.DOFade(1f, animationTime);
        yield return tween.WaitForCompletion();

        var destroyDelay = 2f;

        if (_progress.CurrentLevel == 1)
            _tutorial.gameObject.SetActive(true);

        Destroy(this.gameObject, destroyDelay);
        _playingDialogue = null;
    }

    private IEnumerator SkippingDialogue()
    {
        float animationTime = 0.5f;

        _dialogueUI.interactable = false;
        _dialogueUI.blocksRaycasts = false;
        Tween tween = _dialogueUI.DOFade(0f, animationTime);
        yield return tween.WaitForCompletion();

        _mainUI.interactable = true;
        _mainUI.blocksRaycasts = true;
        tween = _mainUI.DOFade(1f, animationTime);
        yield return tween.WaitForCompletion();

        var destroyDelay = 2f;

        Destroy(this.gameObject, destroyDelay);
    }
}
