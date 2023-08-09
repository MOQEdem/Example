using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;

public class ProductPanelCustom : MonoBehaviour
{
    [SerializeField] private RawImage _productImage;
    [SerializeField] private Image _soldOutButton;
    [SerializeField] private TMP_Text _productNameText;
    [SerializeField] private TMP_Text _productCostText;
    [SerializeField] private TMP_Text _closedText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _soldOutImage;
    [SerializeField] private TMP_Text _multiplicatorText;
    [SerializeField] private Image _multiplicatorBackgrpund;
    [SerializeField] private IAPShopIconGiver _iconGiver;

    private CatalogProduct _product;

    public CatalogProduct Product
    {
        get
        {
            return _product;
        }

        set
        {
            _soldOutImage.gameObject.SetActive(false);

            _product = value;

            _productNameText.text = _iconGiver.GetTitle(value.id);
            _productCostText.text = value.priceValue;

            IsSoldOut = false;
            _soldOutButton.enabled = false;

            _productImage.texture = _iconGiver.GetTexture(value.id);
        }
    }

    public bool IsSoldOut { get; private set; }

    public Action<ProductPanelCustom> ProductBought;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnPurchaseAndConsumeButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnPurchaseAndConsumeButtonClick);
    }

    private void OnPurchaseAndConsumeButtonClick()
    {
#if UNITY_EDITOR
        ProductBought?.Invoke(this);
#else
        Billing.PurchaseProduct(_product.id, (purchaseProductResponse) =>
        {
            Debug.Log($"Purchased {purchaseProductResponse.purchaseData.productID}");

            Billing.ConsumeProduct(purchaseProductResponse.purchaseData.purchaseToken, () =>
            {
                Debug.Log($"Consumed {purchaseProductResponse.purchaseData.productID}");
                SetSoldOutStatus();
            });

            ProductBought?.Invoke(this);
        });
#endif
    }

    public void SetSoldOutStatus()
    {
        IsSoldOut = true;

        _soldOutButton.enabled = true;
        _buyButton.interactable = false;
        _soldOutImage.gameObject.SetActive(true);
    }

    public void SetClosedtatus(int demandedLevel)
    {
        IsSoldOut = true;

        _productCostText.gameObject.SetActive(false);
        _buyButton.interactable = false;
        _closedText.text = demandedLevel.ToString();
        _closedText.gameObject.SetActive(true);
    }

    public void SetMultiplicatorText(int value)
    {
        _multiplicatorText.text = $"x{value}";
        _multiplicatorBackgrpund.gameObject.SetActive(true);
    }
}
