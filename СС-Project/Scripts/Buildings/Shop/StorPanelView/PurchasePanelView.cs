using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanelView : MonoBehaviour
{
    [Header("Button")] 
    [SerializeField] private PurchasePriceView _purchasePriceView;
    [SerializeField] private GameObject _maxView;

    [Header("Slider")] 
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _abilityLevelText;

    private void Awake() => 
        SetMaxView(false);

    public void SetPanelInfo(int amountSpent, int requiredAmount)
    {
        _purchasePriceView.SetAmountSpent(amountSpent.ToString());
        _purchasePriceView.SetRequiredAmount(requiredAmount.ToString());
        if(requiredAmount > 0)
            _slider.value = (float)amountSpent / requiredAmount;
    }

    public void SetStatText(int level) => 
        _abilityLevelText.text = (1 + level).ToString();

    public void SetMaxView(bool isActive)
    {
        _maxView.SetActive(isActive);
        _purchasePriceView.gameObject.SetActive(!isActive);
        if (isActive)
            _slider.value = 1;
    }
}
