using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreLot : MonoBehaviour
{
    [SerializeField] private PlayerStatType _type;
    [SerializeField] private Button _button;

    private PurchasePanelView _view;
    private EventTrigger _eventTrigger;
    private PlayerUpgrade _currentUpgrade;
    private int _currentSpendedResources;

    public PlayerStatType Type => _type;
    public PlayerUpgrade Upgrade => _currentUpgrade;

    public event Action<StoreLot> ButtonDown;
    public event Action ButtonUp;
    public event Action<StoreLot> AllDemandedResourceSpended;

    private void Awake()
    {
        _eventTrigger = _button.GetComponent<EventTrigger>();
        _view = GetComponent<PurchasePanelView>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnButtonDown((PointerEventData)data); });


        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerUp;
        exit.callback.AddListener((data) => { OnButtonUp((PointerEventData)data); });

        _eventTrigger.triggers.Add(entry);
        _eventTrigger.triggers.Add(exit);
    }

    public void SetLotStatus(PlayerUpgrade upgrade, int resourcesSpended)
    {
        _currentUpgrade = upgrade;

        if (upgrade != null)
        {
            _currentSpendedResources = resourcesSpended;
            _view.SetPanelInfo(resourcesSpended, upgrade.Cost);
        }
        else
        {
            _view.SetMaxView(true);
        }
    }

    public void AddSpendResource()
    {
        _currentSpendedResources++;
        _view.SetPanelInfo(_currentSpendedResources, _currentUpgrade.Cost);

        if (_currentSpendedResources >= _currentUpgrade.Cost)
            AllDemandedResourceSpended?.Invoke(this);
    }

    public void UpdateInteractable(bool isBalanceActive)
    {
        if (isBalanceActive && _currentUpgrade != null)
            _button.interactable = true;
        else
            _button.interactable = false;
    }

    public void UpdateStatInfo(int step) => 
        _view.SetStatText(step);

    private void OnButtonDown(PointerEventData data)
    {
        ButtonDown?.Invoke(this);
    }

    private void OnButtonUp(PointerEventData data)
    {
        ButtonUp?.Invoke();
    }
}
