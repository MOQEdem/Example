using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ResourceExchangeCoreView : MonoBehaviour
{
    [SerializeField] private Transform _stackPoint;
    [SerializeField] private float _stepOffset;

    private ResourceExchangeCore _core;

    private void Awake()
    {
        _core = GetComponent<ResourceExchangeCore>();
    }

    private void OnEnable()
    {
        _core.CountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _core.CountChanged -= OnCountChanged;
    }

    private void OnCountChanged(Resource resource)
    {
        StartCoroutine(Moving(resource));
    }

    private Vector3 GetCurrentFreePoint()
    {
        return new Vector3(0, _core.Resources.Count * _stepOffset, 0);
    }

    private IEnumerator Moving(Resource resource)
    {
        float animationDuration = 0.7f;
        resource.transform.DOKill();

        Vector3 direction = (_stackPoint.position - this.transform.position) / 2;
        direction.y = 0;

        Tween move = resource.transform.DOMove(_stackPoint.position - direction, animationDuration);
        yield return move.WaitForCompletion();

        resource.transform.parent = _stackPoint;
        resource.SetTarget(GetCurrentFreePoint());
    }
}
