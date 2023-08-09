using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceExchangeExit : MonoBehaviour
{
    [SerializeField] private ResourceExchangeCore _resourceSource;

    private WaitForSeconds _delay = new WaitForSeconds(0.1f);
    private Coroutine _transmitingResource;
    private Player _player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_player == null)
                _player = player;

            if (_transmitingResource == null)
                _transmitingResource = StartCoroutine(TransmitingResource(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_transmitingResource != null)
            {
                StopCoroutine(_transmitingResource);
                _transmitingResource = null;
            }
        }
    }

    private IEnumerator TransmitingResource(Player player)
    {
        yield return _delay;

        while (_resourceSource.IsHaveExchangedResource && !player.PlayerStack.IsFull)
        {
            if (player.PlayerStack.IsReadyToTransit)
            {
                Resource resource = _resourceSource.SpendResource();
                resource.transform.parent = null;
                player.PlayerStack.AddResourceToWaitList(resource);
            }

            yield return null;
        }

        _transmitingResource = null;
    }
}
