using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoreActivator : MonoBehaviour
{
    [Header("GameData")]
    [SerializeField] private Button _closeButton;
    [SerializeField] private Canvas _worldCanvas;
    [SerializeField] private CanvasGroup _shopUI;
    [Space]
    [Header("Model")]
    [SerializeField] private Transform _shop;
    [SerializeField] private Transform _seller;

    private bool _isOpened = false;
    private Coroutine _setShopStatus;
    private float _fadeDuration = 0.5f;
    private PlayerTrigger _playerTrigger;

    private void Awake()
    {
        _shop.localScale = Vector3.zero;
        _seller.localScale = Vector3.zero;

        _isOpened = false;
        _worldCanvas.gameObject.SetActive(false);
        _playerTrigger = GetComponentInChildren<PlayerTrigger>(true);
    }

    private void OnEnable()
    {
        _playerTrigger.Enter += OnPlayerEnter;
        _playerTrigger.Exit += OnPlayerExit;

        _closeButton.onClick.AddListener(CloseShop);
    }

    private void OnDisable()
    {
        _playerTrigger.Enter -= OnPlayerEnter;
        _playerTrigger.Exit -= OnPlayerExit;

        _closeButton.onClick.RemoveListener(CloseShop);
    }

    private void OnPlayerEnter(Player player)
    {
        StopCoroutine();

        if (_isOpened)
            _setShopStatus = StartCoroutine(SetShopStatus(true));
    }

    private void OnPlayerExit(Player player)
    {
        if (_isOpened)
            CloseShop();
    }

    public void Open(EnemyArmy army)
    {
        StartCoroutine(OpeningShop(army));
    }

    private void StopCoroutine()
    {
        if (_setShopStatus != null)
        {
            StopCoroutine(_setShopStatus);
            _setShopStatus = null;
        }
    }

    private void CloseShop()
    {
        StopCoroutine();

        _setShopStatus = StartCoroutine(SetShopStatus(false));
    }

    private IEnumerator SetShopStatus(bool isOpen)
    {
        float alfaValue = isOpen ? 1.0f : 0.0f;

        _shopUI.DOKill();

        Tween setCanvasAlfa = _shopUI.DOFade(alfaValue, _fadeDuration);
        yield return setCanvasAlfa.WaitForCompletion();

        _shopUI.blocksRaycasts = isOpen;
        _shopUI.interactable = isOpen;
    }

    private IEnumerator OpeningShop(EnemyArmy army)
    {
        float animationTime = 1f;

        transform.position = army.LevelFinisher.transform.position + (Vector3.left * 6f) + (Vector3.forward * 6f);

        _isOpened = true;
        _worldCanvas.gameObject.SetActive(true);

        _shop.transform.DORotate(new Vector3(0, 720, 0), animationTime, RotateMode.FastBeyond360);
        Tween animation = _shop.transform.DOScale(1f, animationTime);
        yield return animation.WaitForCompletion();

        _seller.DORotate(new Vector3(0, 720, 0), animationTime, RotateMode.FastBeyond360);
        _seller.DOJump(_seller.position, 2f, 1, animationTime);
        animation = _seller.DOScale(1f, animationTime);
        yield return animation.WaitForCompletion();
    }
}
