using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsResourceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceDemand;
    [SerializeField] private TMP_Text _resourceAdded;
    [SerializeField] private Image _icon;

    private ResourceHolder _holder;
    private ResourcesData _data;
    private RectTransform _rectTransform;

    public ResourceHolder Resource => _holder;
    public RectTransform RectTransform => _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if (_holder != null)
            _holder.BalanceChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        if (_holder != null)
            _holder.BalanceChanged -= OnValueChanged;
    }

    public void Init(ResourceHolder resourceHolder, ResourcesData data)
    {

        _holder = resourceHolder;
        _data = data;

        _icon.sprite = _data.GetIcon(_holder.Type);

        _resourceAdded.text = (_holder.StartValue - _holder.Value).ToString();
        _resourceDemand.text = "/" + _holder.StartValue.ToString();

        _holder.BalanceChanged += OnValueChanged;

        OnValueChanged(_holder.Value);
    }

    public void SetPosition(float yPosition)
    {
        Vector2 newPosition = _rectTransform.anchoredPosition;
        newPosition.y = yPosition;
        _rectTransform.anchoredPosition = newPosition;
    }

    private void OnValueChanged(int value)
    {
        _resourceAdded.text = (_holder.StartValue - value).ToString();
    }

}
