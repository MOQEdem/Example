using _ProjectAssets.Scripts.Pointers;
using UnityEngine;

public class PointerChainEnabler : MonoBehaviour
{
    [SerializeField] private Pointer _pointerToDisable;
    [SerializeField] private Pointer[] _pointersToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _pointerToDisable.Off();
            foreach (Pointer pointer in _pointersToEnable) 
                pointer.On();
        }
    }
}
