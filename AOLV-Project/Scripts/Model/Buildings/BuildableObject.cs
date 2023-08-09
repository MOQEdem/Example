using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class BuildableObject : MonoBehaviour
{
    [SerializeField] private ResourceSpender _spender;
    [SerializeField] private Resizer _unbuiltObject;
    [SerializeField] private Resizer _builtObject;

    private BuildingStatus _buildingStatus;
    private bool _isIsBuilded;


    protected abstract BuildingStatus InitBuildingStatus();

    public bool IsBuilded => _isIsBuilded;

    public event UnityAction Builded;

    private void Awake()
    {
        _buildingStatus = InitBuildingStatus();
        _buildingStatus.Load();

        _isIsBuilded = _buildingStatus.IsBuilded;
    }

    private void Start()
    {
        if (_buildingStatus.IsBuilded == false)
        {
            _builtObject.gameObject.SetActive(false);
            _spender.Spended += OnResourcesSpend;
        }
        else
        {
            _unbuiltObject.gameObject.SetActive(false);
        }
    }

    private void OnResourcesSpend()
    {
        _buildingStatus.SetBuildedStatus();
        _buildingStatus.Save();
        _spender.Spended -= OnResourcesSpend;

        StartCoroutine(Building());
    }

    private IEnumerator Building()
    {
        yield return _unbuiltObject.ResizeCoroutine(0.4f, Vector3.zero);

        _unbuiltObject.gameObject.SetActive(false);


        _builtObject.gameObject.transform.localScale = Vector3.zero;
        _builtObject.gameObject.SetActive(true);

        yield return _builtObject.ResizeCoroutine(0.4f, Vector3.one);

        Builded?.Invoke();
    }
}
