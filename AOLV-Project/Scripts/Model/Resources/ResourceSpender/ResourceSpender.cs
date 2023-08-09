using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ResourceSpender : MonoBehaviour
{
    [SerializeField] private Trigger<Player> _trigger;
    [SerializeField] private ResourceHolder[] _requiredResources;
    [SerializeField] private float _delayBeforeSpendResources = 1f;
    [SerializeField] private Transform _spawnPoint;

    private Coroutine[] _takingResources;
    private bool _isAllResourcesSpended = false;

    public ResourceHolder[] ResourceHolders => _requiredResources;

    public event UnityAction Spended;

    private void Awake()
    {
        _requiredResources = GetComponents<ResourceHolder>();
        _takingResources = new Coroutine[_requiredResources.Length];
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
        _trigger.Exit += OnPlayerExit;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
        _trigger.Exit -= OnPlayerExit;
    }

    private void OnPlayerEnter(Player player)
    {
        player.HubResources.SetSpendingStatus(true);
        for (int i = 0; i < _requiredResources.Length; i++)
        {
            if (_requiredResources[i].Value == 0)
                continue;

            if (_takingResources[i] == null)
                _takingResources[i] = StartCoroutine(TakingResources(player, _requiredResources[i], i));
        }

        TryPlayEvent(player);
    }

    private void OnPlayerExit(Player player)
    {
        player.HubResources.SetSpendingStatus(false);

        for (int i = 0; i < _requiredResources.Length; i++)
        {
            if (_takingResources[i] != null)
            {
                StopCoroutine(_takingResources[i]);
                _takingResources[i] = null;
            }
        }
    }

    private bool IsAllResourcesSpended()
    {
        foreach (var resource in _requiredResources)
        {
            if (resource.Value > 0)
                return false;
        }

        return true;
    }

    private bool IsPlayerAbleToSpendResources(Player player, ResourceType unverifiableType)
    {
        foreach (var resource in _requiredResources)
        {
            if (resource.Type != unverifiableType && resource.Value > 0)
            {
                if (player.HubResources.IsAbleToSpendResource(new ResourcePack(1, resource.Type)))
                    return true;
            }
        }

        return false;
    }

    private void TryPlayEvent(Player player)
    {
        if (IsAllResourcesSpended() && !_isAllResourcesSpended)
        {
            player.HubResources.SetSpendingStatus(false);
            _isAllResourcesSpended = true;
            _trigger.Enter -= OnPlayerEnter;
            Spended?.Invoke();
            _trigger.Hide();
        }
    }

    private IEnumerator TakingResources(Player player, ResourceHolder holder, int coroutineNumber)
    {
        yield return new WaitForSeconds(_delayBeforeSpendResources);

        var delayBetweenSpending = new WaitForSeconds(0.25f);

        var minResourcePack = new ResourcePack(1, holder.Type);

        while (holder.Value > 0 && player.HubResources.IsAbleToSpendResource(minResourcePack))
        {
            var firstTryResourcePack = new ResourcePack(17, holder.Type);

            if (firstTryResourcePack.Value > holder.Value)
                firstTryResourcePack = new ResourcePack(holder.Value, holder.Type);

            if (player.HubResources.IsAbleToSpendResource(firstTryResourcePack))
            {
                holder.Spend(player.HubResources.SpendResources(firstTryResourcePack, _spawnPoint));

                yield return delayBetweenSpending;
                continue;
            }

            var secondTryResourcePack = new ResourcePack(7, holder.Type);

            if (player.HubResources.IsAbleToSpendResource(secondTryResourcePack))
            {
                holder.Spend(player.HubResources.SpendResources(secondTryResourcePack, _spawnPoint));

                yield return delayBetweenSpending;
                continue;
            }

            if (player.HubResources.IsAbleToSpendResource(minResourcePack))
            {
                holder.Spend(player.HubResources.SpendResources(minResourcePack, _spawnPoint));

                yield return delayBetweenSpending;
                continue;
            }
        }

        if (!IsPlayerAbleToSpendResources(player, holder.Type))
            player.HubResources.SetSpendingStatus(false);

        yield return null;

        TryPlayEvent(player);

        _takingResources[coroutineNumber] = null;

        TrySetSpendingStatus(player);
    }

    private void TrySetSpendingStatus(Player player)
    {
        foreach (var coroutine in _takingResources)
        {
            if (coroutine != null)
                return;
        }

        player.HubResources.SetSpendingStatus(false);
    }
}
