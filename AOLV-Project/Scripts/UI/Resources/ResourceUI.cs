using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceHolder _holder;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _icon;
    [SerializeField] private ResourcesData _data;

    private RectTransform _rectTransform;
    private float _transformYPosition;
    private Coroutine _movingToPosition;
    private float _movingSpeed = 150f;

    public ResourceHolder Resource => _holder;
    public RectTransform RectTransform => _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _transformYPosition = _rectTransform.anchoredPosition.y;
    }

    private void OnEnable()
    {
        _holder.BalanceChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _holder.BalanceChanged -= OnValueChanged;

    }

    private void Start()
    {
        _icon.sprite = _data.GetIcon(_holder.Type);
        OnValueChanged(_holder.Value);
    }

    public void Activate()
    {
        _text.gameObject.SetActive(true);
        _icon.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _text.gameObject.SetActive(false);
        _icon.gameObject.SetActive(false);
    }

    public void SetPosition(float yPosition)
    {
        _transformYPosition = yPosition;

        if (_movingToPosition == null)
            StartCoroutine(MovingToPosition());
    }

    private void OnValueChanged(int value)
    {
        _text.text = value.ToString();
    }

    private IEnumerator MovingToPosition()
    {
        while (_rectTransform.anchoredPosition.y != _transformYPosition)
        {
            Vector2 newPosition = _rectTransform.anchoredPosition;

            newPosition.y = Mathf.MoveTowards(_rectTransform.anchoredPosition.y, _transformYPosition, _movingSpeed * Time.deltaTime);

            _rectTransform.anchoredPosition = newPosition;

            yield return null;
        }

        _movingToPosition = null;
    }
}