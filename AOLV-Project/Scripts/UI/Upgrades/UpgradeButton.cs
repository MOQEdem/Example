using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UpgradeButton : MonoBehaviour
{
    [SerializeField] private ResourcesData _data;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _spendingPoint;
    [SerializeField] private Trigger<Player> _trigger;

    [SerializeField] protected Player Player;
    [SerializeField] protected Button Button;

    protected int CurrentStep = 0;

    private string _compliteCostText = "-";

    protected abstract bool IsComplete();
    protected abstract bool IsEnoughResources();
    protected abstract ResourcePack UpgradeCost();
    protected abstract string SaveID();

    public event UnityAction Upgraded;

    private void Awake()
    {
        CurrentStep = PlayerPrefs.GetInt(SaveID(), 0);
    }

    private void OnEnable()
    {
        _trigger.Enter += OnPlayerEnter;

        Button.onClick.AddListener(OnButtonClicked);
        UpdateCostView();
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnPlayerEnter;

        Button.onClick.RemoveListener(OnButtonClicked);
        UpdateCostView();
    }

    protected virtual void Start()
    {
        CheckUpgradePossibility();
        UpdateCostView();
    }

    protected virtual void OnButtonClicked()
    {
        TakeCost();

        CurrentStep++;

        PlayerPrefs.SetInt(SaveID(), CurrentStep);

        SendAction();

        UpdateCostView();

        CheckUpgradePossibility();
    }

    protected void CheckUpgradePossibility()
    {
        if (IsComplete() || !IsEnoughResources())
            Button.interactable = false;
        else
            Button.interactable = true;
    }

    protected void UpdateCostView()
    {
        if (!IsComplete())
        {
            _cost.text = UpgradeCost().Value.ToString();
            _image.sprite = _data.GetIcon(UpgradeCost().Type);
        }
        else
        {
            _cost.text = _compliteCostText;
            _image.gameObject.SetActive(false);
        }
    }

    protected void TakeCost()
    {
        Player.HubResources.SpendResources(UpgradeCost(), _spendingPoint);
    }

    protected void SendAction()
    {
        Upgraded?.Invoke();
    }

    private void OnPlayerEnter(Player player)
    {
        CheckUpgradePossibility();
        UpdateCostView();
    }
}
