using UnityEngine;

public class ResourceListUI : MonoBehaviour
{
    [SerializeField] private ResourceUI[] _resources;

    private float _startYPosition;
    private float _stepYPosition;

    private void OnEnable()
    {
        foreach (var resource in _resources)
            resource.Resource.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        foreach (var resource in _resources)
            resource.Resource.BalanceChanged -= OnBalanceChanged;
    }

    private void Start()
    {
        _startYPosition = _resources[0].RectTransform.anchoredPosition.y;
        _stepYPosition = _resources[0].RectTransform.rect.height;

        SortResources();
        SetResourcesActivity();
        SetResourcesPosition();
    }

    private void OnBalanceChanged(int value)
    {
        SortResources();
        SetResourcesActivity();
        SetResourcesPosition();
    }

    private void SortResources()
    {
        int index;

        for (int i = 0; i < _resources.Length; i++)
        {
            index = i;

            for (int j = i; j < _resources.Length; j++)
            {
                if (_resources[j].Resource.Value > _resources[index].Resource.Value)
                {
                    index = j;
                }
            }

            if (_resources[index] != _resources[i])
            {
                ResourceUI temp = _resources[i];
                _resources[i] = _resources[index];
                _resources[index] = temp;
            }
        }
    }

    private void SetResourcesActivity()
    {
        foreach (var resource in _resources)
        {
            if (resource.Resource.Value > 0)
                resource.Activate();
            else
                resource.Deactivate();
        }
    }

    private void SetResourcesPosition()
    {
        float position = _startYPosition;

        for (int i = 0; i < _resources.Length; i++)
        {
            _resources[i].SetPosition(position);

            position -= _stepYPosition;
        }
    }
}
