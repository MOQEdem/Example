using UnityEngine;

public abstract class InteractiveZoneProgressLoader : MonoBehaviour
{
    [SerializeField] private Resource _resource;

    private InteractiveZoneProgress _progress;
    private InteractiveZone _zone;

    protected abstract InteractiveZoneProgress InitInteractiveZoneProgress();

    private void Awake()
    {
        _zone = GetComponent<InteractiveZone>();

        _progress = InitInteractiveZoneProgress();
        _progress.Load();

        int resourcesAlreadyAdded = _progress.ResourceCount;

        for (int i = 0; i < resourcesAlreadyAdded; i++)
        {
            Resource resource = Instantiate(_resource, _zone.transform);
            resource.transform.parent = null;
            resource.PickUp();

            _zone.LoadResorces(resource);
        }
    }

    public void SaveValue()
    {
        if (_progress != null)
        {
            _progress.SetResourceCount(_zone.Resources.Count);
            _progress.Save();
        }
    }
}
