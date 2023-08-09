using UnityEngine;

public abstract class HeapHolder : MonoBehaviour
{
    private Heap _heap;

    protected abstract Heap InitHeap();

    private void OnEnable()
    {
        _heap = InitHeap();
        _heap.Load();
        
        if (_heap.Cooldown > 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _heap.Save();
    }

    public void Collected()
    {
        _heap.SetCooldown();
        _heap.Save();
    }
}