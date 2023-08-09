using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerWarehouse : MonoBehaviour
{
    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private Trigger<Player> _trigger;
    [SerializeField] private Transform _spawnPoint;

    public ResourceHolder Resource => _resourceHolder;

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;
    }

    private void OnPlayerEnter(Player player)
    {
        if (_resourceHolder.Value > 0)
        {
            if (player.HubResources.IsAbleToTakeResource(new ResourcePack(_resourceHolder.Value, _resourceHolder.Type), out int possibleToAdd))
            {
                Debug.Log(_resourceHolder.Value);
                Debug.Log(possibleToAdd);

                int resourceToAdd = Mathf.Clamp(_resourceHolder.Value, 0, possibleToAdd);
                Debug.Log(resourceToAdd);
                player.HubResources.TakeResource(new ResourcePack(resourceToAdd, _resourceHolder.Type), _spawnPoint);
                _resourceHolder.Spend(resourceToAdd);
            }
        }
    }
}
