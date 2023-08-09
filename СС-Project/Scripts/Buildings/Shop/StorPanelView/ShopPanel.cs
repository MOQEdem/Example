using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private PurchasePanelView _healthPanel;
    [SerializeField] private PurchasePanelView _damagePanel;
    [SerializeField] private PurchasePanelView _speedPanel;
    [SerializeField] private PurchasePanelView _buffRadiusPanel;
    [SerializeField] private PurchasePanelView _stackCapacityPanel;

    public PurchasePanelView Health => _healthPanel;
    public PurchasePanelView Damage => _damagePanel;
    public PurchasePanelView Speed => _speedPanel;
    public PurchasePanelView Radius => _buffRadiusPanel;
    public PurchasePanelView Capacity => _stackCapacityPanel;
}
