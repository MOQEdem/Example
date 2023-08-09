using UnityEngine;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] _buttons;
    [SerializeField] private Button _next;
    [SerializeField] private Button _previous;

    private int _currentButton = 0;

    private void OnEnable()
    {
        _next.onClick.AddListener(OnNextButtonClick);
        _previous.onClick.AddListener(OnPreviousButtonClick);
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(OnNextButtonClick);
        _previous.onClick.RemoveListener(OnPreviousButtonClick);
    }

    private void Start()
    {
        HideAllButtons();
        ShowActive();
    }

    private void OnNextButtonClick()
    {
        _currentButton++;

        if (_currentButton > _buttons.Length - 1)
        {
            _currentButton = 0;
        }

        HideAllButtons();
        ShowActive();
    }

    private void OnPreviousButtonClick()
    {
        _currentButton--;

        if (_currentButton < 0)
        {
            _currentButton = _buttons.Length - 1;
        }

        HideAllButtons();
        ShowActive();
    }

    private void HideAllButtons()
    {
        foreach (var button in _buttons)
        {
            button.blocksRaycasts = false;
            button.interactable = false;
            button.alpha = 0;
        }
    }

    private void ShowActive()
    {
        _buttons[_currentButton].interactable = true;
        _buttons[_currentButton].blocksRaycasts = true;
        _buttons[_currentButton].alpha = 1f;
    }

}
