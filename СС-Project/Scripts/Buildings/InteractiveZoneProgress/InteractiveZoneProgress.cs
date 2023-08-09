using UnityEngine;

public class InteractiveZoneProgress : SavedObject<InteractiveZoneProgress>
{
    [SerializeField] private int _resourceCount = 0;

    public InteractiveZoneProgress(string guid)
        : base(guid)
    { }

    public int ResourceCount => _resourceCount;

    public void SetResourceCount(int count)
    {
        _resourceCount = count;
    }

    protected override void OnLoad(InteractiveZoneProgress loadedObject)
    {
        _resourceCount = loadedObject.ResourceCount;
    }
}
