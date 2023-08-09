using UnityEngine;
using TMPro;
using DG.Tweening;

public class FireballCounter : MonoBehaviour
{
    [SerializeField] private FireballCounterUI _ui;

    private TMP_Text _text;
    private Dragon _dragon;
    private CanvasGroup _canvasGroup;
    private float _fadeTime = 0.5f;

    private void Awake()
    {
        _text = _ui.GetComponentInChildren<TMP_Text>();
        _canvasGroup = _ui.GetComponent<CanvasGroup>();
        _dragon = GetComponent<Dragon>();
    }

    private void OnEnable()
    {
        _dragon.NumberOfFireBallsChanged += OnNumberOfFireBallsChanged;
        _dragon.FlyingEnd += OnFlyingEnd;
    }

    private void OnDisable()
    {
        _dragon.NumberOfFireBallsChanged -= OnNumberOfFireBallsChanged;
        _dragon.FlyingEnd -= OnFlyingEnd;
    }

    private void OnNumberOfFireBallsChanged()
    {
        if (_dragon.CurrentNumberOfFireBalls > 0 && _canvasGroup.alpha != 1f)
        {
            _canvasGroup.DOFade(1f, _fadeTime);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        else if (_dragon.CurrentNumberOfFireBalls == 0 && _canvasGroup.alpha != 0f)
        {
            _canvasGroup.DOFade(0f, _fadeTime);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        _text.text = _dragon.CurrentNumberOfFireBalls.ToString();
    }

    private void OnFlyingEnd()
    {
        _canvasGroup.DOFade(0f, _fadeTime);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
