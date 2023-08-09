using TMPro;
using UnityEngine;

public class BuildingsResourceListUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private BuildingsResourceUI _buildingsRecourceUIPrefab;
    [SerializeField] private ResourcesData _data;
    [SerializeField] private ResourceSpender _spender;
    [SerializeField] private BuildableObject _buildableObject;
    [SerializeField] private CanvasGroup _canvas;

    private HiderUI _hider;
    private ResourceHolder[] _holders;
    private float _startYPosition = -1.5f;
    private float _stepYPosition;
    private float _heightUI;
    private RectTransform _rectTransform;

    private void OnEnable()
    {
        _spender.Spended += OnSpended;
    }

    private void OnDisable()
    {
        _spender.Spended -= OnSpended;
    }

    private void Start()
    {
        _hider = new HiderUI();
        // _nameText.text = _buildableObject.name;
        _holders = _spender.ResourceHolders;
        _rectTransform = GetComponent<RectTransform>();
        InitResources();
    }

    private void InitResources()
    {
        float newUIPosition = _startYPosition;

        foreach (var holder in _holders)
        {
            var resourceUI = Instantiate(_buildingsRecourceUIPrefab, this.transform);

            _stepYPosition = resourceUI.RectTransform.rect.height;

            resourceUI.Init(holder, _data);
            resourceUI.SetPosition(newUIPosition);
            newUIPosition -= _stepYPosition;
        }

        _heightUI = newUIPosition;

        Vector2 newUISize = _rectTransform.sizeDelta;

        newUISize.y = Mathf.Abs(_heightUI);

        _rectTransform.sizeDelta = newUISize;
    }

    private void OnSpended()
    {
        if (_canvas != null)
            StartCoroutine(_hider.Hiding(_canvas));
    }
}
