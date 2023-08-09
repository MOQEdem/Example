using System.Collections;
using _ProjectAssets.Scripts.Pointers;
using UnityEngine;

public class PointerDelayEnabler : MonoBehaviour
{
    [SerializeField] private Pointer _pointerToEnable;
    [SerializeField] [Min(0)] private float _delay = 0.1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player)) 
            StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        _pointerToEnable.On();
    }
}
