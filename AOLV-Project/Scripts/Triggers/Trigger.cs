using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trigger<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;

    private HiderUI _hider;

    public event UnityAction<T> Enter;
    public event UnityAction<T> Exit;

    private Collider _collider;

    private void Awake()
    {
        _hider = new HiderUI();
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T triggered))
        {
            Enter?.Invoke(triggered);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T triggeredObject))
        {
            Exit?.Invoke(triggeredObject);
        }
    }

    public void Hide()
    {
        if (_canvas != null)
            StartCoroutine(_hider.Hiding(_canvas));
    }
}
