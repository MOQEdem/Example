using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IAPShopButtonView : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void SetNumberOfAvailableProducts(int numberOfProducts)
    {
        if (numberOfProducts == 0)
        {
            _icon.gameObject.SetActive(false);
        }
        else
        {
            _text.text = numberOfProducts.ToString();
            _icon.gameObject.SetActive(true);
        }
    }
}
