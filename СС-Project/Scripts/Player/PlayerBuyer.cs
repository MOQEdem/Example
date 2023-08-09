using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyer : MonoBehaviour
{
    private PlayerStack _stack;
    private List<InteractiveZone> _zones = new List<InteractiveZone>();
    private InteractiveZone _currentZone;
    private WaitForSeconds _delay = new WaitForSeconds(0.1f);
    private Coroutine _spendingResources;


    private void Awake()
    {
        _stack = GetComponent<PlayerStack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<InteractiveZone>(out InteractiveZone zone))
        {
            _zones.Add(zone);

            OnZonesCountChange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<InteractiveZone>(out InteractiveZone zone))
        {
            _zones.Remove(zone);

            OnZonesCountChange();
        }
    }

    private void OnZonesCountChange()
    {
        StopSpending();

        if (_zones.Count > 0)
        {
            _currentZone = _zones[0];

            if (_currentZone.IsFilled == false)
                StartSpending();
        }
    }

    private void StartSpending()
    {
        if (_spendingResources == null)
        {
            _currentZone.Ended += StopSpending;

            if (_currentZone.ResourceType == ResourceType.Player)
                _spendingResources = StartCoroutine(SpendingResources(_currentZone, _stack.PlayerResource));
            if (_currentZone.ResourceType == ResourceType.Enemy)
                _spendingResources = StartCoroutine(SpendingResources(_currentZone, _stack.EnemyResource));
        }
    }

    private void StopSpending()
    {
        if (_currentZone != null)
        {
            _currentZone.Ended -= StopSpending;

            if (_spendingResources != null)
            {
                StopCoroutine(_spendingResources);
                _spendingResources = null;
            }

            _currentZone = null;
        }
    }

    private IEnumerator SpendingResources(InteractiveZone zone, List<Resource> resources)
    {
        bool isWaitedBeforSpend = false;

        while (resources.Count > 0 && zone.IsFilled == false)
        {
            if (_stack.IsReadyToTransit)
            {
                if (!isWaitedBeforSpend)
                {
                    yield return new WaitForSeconds(0.23f);
                    isWaitedBeforSpend = true;
                }

                zone.ApplyResource(_stack.Transit(resources), true);
            }

            if (zone.IsFilled == true)
            {
                _zones.Remove(zone);
            }

            yield return _delay;
        }

        _spendingResources = null;
    }
}
