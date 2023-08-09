using UnityEngine;

public abstract class ConstructionLine : MonoBehaviour
{
    [SerializeField] private BuildableObject[] _objects;

    private ConstructionPhase _phase;

    protected abstract ConstructionPhase InitPhase();

    private void Awake()
    {
        _phase = InitPhase();
        _phase.Load();

        foreach (var building in _objects)
        {
            building.Builded += OnObjectBuild;
        }
    }

    private void OnDisable()
    {

        foreach (var building in _objects)
        {
            building.Builded -= OnObjectBuild;
        }
    }

    private void Start()
    {
        for (int i = _phase.Value + 1; i < _objects.Length; i++)
        {
            _objects[i].gameObject.SetActive(false);
        }
    }

    private void OnObjectBuild()
    {
        if (_objects.Length > _phase.Value + 1)
        {
            _phase.SetNextPhase();

            _objects[_phase.Value].gameObject.SetActive(true);
        }
    }
}
