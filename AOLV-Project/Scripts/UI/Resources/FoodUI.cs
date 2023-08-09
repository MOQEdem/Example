using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;
    [SerializeField] private ResourcesData _resourcesData;
    [SerializeField] private ResourceHolder _resourceHolder;
    
    private ResourceType _type = ResourceType.Food;

    private void OnEnable()
    {
        _image.sprite = _resourcesData.GetIcon(_type);

        _resourceHolder.BalanceChanged += OnBalanceChanged;

        _slider.minValue = 0;
        _slider.maxValue = _resourceHolder.MaxValue;
    }

    private void OnDisable()
    {
        _resourceHolder.BalanceChanged -= OnBalanceChanged;
    }

    private void Start()
    {
        OnBalanceChanged(_resourceHolder.Value);
    }

    private void OnBalanceChanged(int value)
    {
        _slider.value = value;
    }
}
