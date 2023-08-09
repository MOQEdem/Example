using System.Collections;
using UnityEngine;

public class WarehouseView : MonoBehaviour
{
    [SerializeField] private ResourceHolder _holder;
    [SerializeField] private ResourcesData _data;
    [SerializeField] private WarehouseViewPoint[] _points;
    [SerializeField] private int _valueToPlaceOnePoint;
    [SerializeField] private Resizer _text;

    private int _currentPointsCount = -1;
    private int _targetPointsCount;
    private Coroutine _spawningResources;
    private ResourceModel _model;
    private int _maxPointsCount;
    private Vector3 _normalTextScale;

    private void OnEnable()
    {
        _holder.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable()
    {
        _holder.BalanceChanged -= OnBalanceChanged;
    }

    private void Start()
    {
        if (_text != null)
            _normalTextScale = _text.transform.localScale;

        _model = _data.GetResourceModel(_holder.Type);
        _maxPointsCount = _points.Length;
        OnBalanceChanged(_holder.Value);
    }

    private void OnBalanceChanged(int value)
    {
        int takenPoints = (value / _valueToPlaceOnePoint);

        if (takenPoints > _maxPointsCount)
            takenPoints = _maxPointsCount;

        _targetPointsCount = takenPoints - 1;

        int changedPoints = _targetPointsCount - _currentPointsCount;

        if (changedPoints == 0)
            return;

        if (_spawningResources == null)
            _spawningResources = StartCoroutine(SpawningResources(changedPoints, takenPoints));
    }

    private IEnumerator SpawningResources(int changedPoints, int takenPoints)
    {
        var delay = new WaitForSeconds(0.3f);

        while (_targetPointsCount != _currentPointsCount)
        {
            if (_text != null)
            {
                if (_targetPointsCount >= 0)
                    _text.Resize(1f, 0);
                else
                    _text.Resize(1f, _normalTextScale);
            }

            if (_targetPointsCount - _currentPointsCount > 0)
            {
                for (int i = _currentPointsCount + 1; i <= _targetPointsCount; i++)
                {
                    _points[i].PlaceResource(_model);
                    _currentPointsCount++;
                    yield return delay;
                }
            }
            else if (_targetPointsCount - _currentPointsCount < 0)
            {
                for (int i = _currentPointsCount; i > _targetPointsCount; i--)
                {
                    _points[i].Resizer.Resize(1f, Vector3.zero);
                    _points[i].ReplaceResource(0.3f);
                    _currentPointsCount--;
                }
            }
        }

        _spawningResources = null;
    }
}
