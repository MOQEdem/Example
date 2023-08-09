using System.Collections;
using UnityEngine;

public class StorageExit : MonoBehaviour
{
    [SerializeField] private StorageEnter _resourceSource;

    private WaitForSeconds _delay = new WaitForSeconds(0.1f);
    private Coroutine _transmitingResource;

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Player>(out Player player))
        //  if (_transmitingResource == null)
        {
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

        while (_resourceSource.Resources.Count > 0)
        {
            if (player.PlayerStack.IsReadyToTransit && !player.PlayerStack.IsFull)
            {
                Resource resource = _resourceSource.SpendResource();
                resource.transform.parent = null;
                player.PlayerStack.AddResourceToWaitList(resource);
            }
            yield return _delay;
        }

        _transmitingResource = null;
    }
}
