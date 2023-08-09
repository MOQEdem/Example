using UnityEngine;
using UnityEngine.Events;

public abstract class ResourceHolder : MonoBehaviour
{
    [SerializeField] private int _maxValue;

    private Resource _resource;

    public event UnityAction<int> BalanceChanged;

    protected abstract Resource InitResource();
    protected abstract int InitStartValue();

    public int MaxValue => _maxValue;
    public bool IsHaveLimit => _maxValue != 0;
    public int Value => _resource.Value;
    public ResourceType Type => InitResource().Type;
    public bool HasResource => _resource.Value > 0;
    public int StartValue => InitStartValue();

    private void Awake()
    {
        _resource = InitResource();
        _resource.Load();

        if (!PlayerPrefs.HasKey(_resource.GUID) == true)
            Add(InitStartValue());
    }

    private void OnEnable()
    {
        _resource.Changed += OnBalanceChanged;
    }

    private void OnDisable()
    {
        _resource.Changed -= OnBalanceChanged;
        _resource.Save();
    }

    public void Add(int value)
    {
        if (!IsHaveLimit)
        {
            _resource.Add(value);
        }
        else
        {
            int valueAbleToAdd = _maxValue - _resource.Value;
            valueAbleToAdd = Mathf.Clamp(value, 0, valueAbleToAdd);
            _resource.Add(valueAbleToAdd);
        }
    }

    public void Spend(int value)
    {
        _resource.Spend(value);
    }

    private void OnBalanceChanged()
    {
        BalanceChanged?.Invoke(_resource.Value);
        _resource.Save();
    }
}
