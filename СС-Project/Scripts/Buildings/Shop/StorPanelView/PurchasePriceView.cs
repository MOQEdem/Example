using TMPro;
using UnityEngine;

public class PurchasePriceView : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountSpentText;
    [SerializeField] private TMP_Text _requiredAmountText;

    public void SetAmountSpent(string text) => 
        _amountSpentText.text = text;

    public void SetRequiredAmount(string text) => 
        _requiredAmountText.text = text;
}
