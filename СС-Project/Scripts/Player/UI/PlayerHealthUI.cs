using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealthUIBar _playerHealthUI;

    private CanvasGroup _barView;
    private PlayableCharacter _player;
    private Image _bar;

    private void Awake()
    {
        _player = GetComponent<PlayableCharacter>();
        _bar = _playerHealthUI.GetComponent<Image>();
        _barView = GetComponentInChildren<CanvasGroup>();
    }

    private void OnEnable()
    {
        _player.HealthUpdate += OnHealthUpdate;
    }

    private void OnDisable()
    {
        _player.HealthUpdate += OnHealthUpdate;
    }

    private void OnHealthUpdate()
    {
        _bar.fillAmount = _player.HealthFullness;

        if (_bar.fillAmount == 1)
        {
            if (_barView.alpha == 1)
            {
                _barView.DOKill();
                _barView.DOFade(0f, 0.5f);
            }
        }
        else
        {
            if (_barView.alpha == 0)
            {
                _barView.DOKill();
                _barView.DOFade(1f, 0.5f);
            }
        }
    }
}
