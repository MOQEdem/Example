using UnityEngine;

public class EmptyBagTransition : Transition
{
    [SerializeField] private ResourceBag _resourceBag;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _resourceBag.HolderEmpty += OnHolderEmpty;
    }
    
    private void OnDisable()
    {
        _resourceBag.HolderEmpty -= OnHolderEmpty;
    }
    
    private void OnHolderEmpty(ResourceType resourceData)
    {
        NeedTransite = true;
    }
}
