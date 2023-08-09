using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResourceRecycler : MonoBehaviour
{
    [SerializeField] private ResourceHolder _resourceRequiredHolder;
    [SerializeField] private int _resourceValueRequired;
    [SerializeField] private ResourceHolder _resourceProducedHolder;
    [SerializeField] private int _resourceValueProduced;
    [SerializeField] private float _delayBeforeSpendResources = 1f;
    [SerializeField] private float _timeForProduction = 1f;
    [SerializeField] private Trigger<Player> _trigger;
    [SerializeField] private Transform _spawningPoint;
    [SerializeField] private RecyclerAnimation _animation;

    private ResourcePack _resourcePackNeeded;
    private ResourcePack _resourcePackProduces;
    private Coroutine _gatherResource;
    private Coroutine _recyclingResources;
    private bool _isProcessActive;

    public ResourceType RequiredResourceType => _resourceRequiredHolder.Type;
    public int RequiredResourceValue => _resourceValueRequired;
    public ResourceType ProducedResourceType => _resourceProducedHolder.Type;
    public int ProducedResourceValue => _resourceValueProduced;

    // public event UnityAction ResourceProduced;

    private void Awake()
    {
        _resourcePackNeeded = new ResourcePack(_resourceValueRequired, _resourceRequiredHolder.Type);
        _resourcePackProduces = new ResourcePack(_resourceValueProduced, _resourceProducedHolder.Type);
    }

    private void OnEnable()
    {
        _resourceRequiredHolder.BalanceChanged += OnBalanceChanged;
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _resourceRequiredHolder.BalanceChanged -= OnBalanceChanged;
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
    }

    private void Start()
    {
        OnBalanceChanged(_resourceRequiredHolder.Value);
    }

    private void OnPlayerEnter(Player player)
    {
        player.ResourceBag.SetSpendingStatus(true);

        if (_gatherResource == null)
        {
            _isProcessActive = true;
            _gatherResource = StartCoroutine(GatherResource(player));
        }
    }

    private void OnPlayerExit(Player player)
    {
        player.ResourceBag.SetSpendingStatus(false);

        if (_gatherResource != null)
        {
            StopCoroutine(_gatherResource);
            _isProcessActive = false;
            _gatherResource = null;
        }
    }

    private void OnBalanceChanged(int balance)
    {
        if (_resourcePackNeeded.Value > balance)
            return;

        if (_recyclingResources == null)
        {
            _recyclingResources = StartCoroutine(RecyclingResources());
        }
    }

    private IEnumerator GatherResource(Player player)
    {
        yield return new WaitForSeconds(_delayBeforeSpendResources);

        var delayBetweenSpending = new WaitForSeconds(0.2f);

        int resourcesAccumulated = 0;
        var resourcePack = new ResourcePack(1, _resourceRequiredHolder.Type);

        while (_isProcessActive)
        {
            if (player.HubResources.IsAbleToSpendResource(resourcePack))
            {
                _resourceRequiredHolder.Add(player.HubResources.SpendResources(resourcePack, _spawningPoint));

                resourcesAccumulated += resourcePack.Value;

                if (resourcesAccumulated > 10 && resourcePack.Value == 1)
                {
                    resourcePack = new ResourcePack(10, _resourceRequiredHolder.Type);
                }
                else if (resourcesAccumulated > 200 && resourcePack.Value == 10)
                {
                    resourcePack = new ResourcePack(100, _resourceRequiredHolder.Type);
                }

                yield return delayBetweenSpending;
            }
            else
            {
                if (resourcePack.Value > 1)
                {
                    resourcePack = new ResourcePack(1, _resourceRequiredHolder.Type);
                }
                else
                {
                    _isProcessActive = false;
                }
            }
        }

        player.HubResources.SetSpendingStatus(false);

        _gatherResource = null;
    }

    private IEnumerator RecyclingResources()
    {
        var delay = new WaitForSeconds(_timeForProduction);

        _animation.StartAimation();

        while (_resourcePackNeeded.Value <= _resourceRequiredHolder.Value)
        {
            yield return delay;

            _resourceRequiredHolder.Spend(_resourcePackNeeded.Value);
            _resourceProducedHolder.Add(_resourcePackProduces.Value);

            yield return null;
        }

        _animation.StopAnimation();

        _recyclingResources = null;
    }
}
