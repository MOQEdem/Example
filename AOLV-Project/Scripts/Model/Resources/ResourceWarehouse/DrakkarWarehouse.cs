using UnityEngine;
using UnityEngine.SceneManagement;

public class DrakkarWarehouse : MonoBehaviour
{
    [SerializeField] private ResourceHolder[] _resourceHolders;
    [SerializeField] private ResourceBag _hubResources;
    [SerializeField] private Trigger<ResourceBag> _trigger;
    [SerializeField] private Transform _spawnPoint;

    private void Awake()
    {
        _resourceHolders = GetComponentsInChildren<ResourceHolder>();
    }

    private void OnEnable()
    {
        if (_trigger != null)
            _trigger.Enter += OnResourceBagEnter;
    }

    private void OnDisable()
    {
        if (_trigger != null)
            _trigger.Enter -= OnResourceBagEnter;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == SceneName.NewHub)
            RemoveAllResources();
    }

    public void RemoveAllResources()
    {
        foreach (var resourceHolder in _resourceHolders)
        {
            if (resourceHolder.Value > 0)
                resourceHolder.Spend(resourceHolder.Value);
        }
    }

    private void OnResourceBagEnter(ResourceBag bag)
    {
        foreach (var resource in _resourceHolders)
        {
            if (bag.IsAbleToSpendResource(new ResourcePack(1, resource.Type)))
            {
                int takingResources = bag.SpendResources(new ResourcePack(int.MaxValue, resource.Type), transform);
                resource.Add(takingResources);
                _hubResources.TakeResource(new ResourcePack(takingResources, resource.Type), transform);
            }
        }
    }
}
