using UnityEngine;

public class BagFillingTransition : Transition
{
    [SerializeField] private ResourceBag _resourceBag;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _resourceBag.HolderFilled += OnHolderFilled;
    }
    
    private void OnDisable()
    {
        _resourceBag.HolderFilled -= OnHolderFilled;
    }
    
    private void OnHolderFilled()
    {
        NeedTransite = true;
    }
}
