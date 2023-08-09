using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;
using System.Collections;
using Agava.YandexMetrica;

public class ProductCatalogPanelCustom : MonoBehaviour
{
    [SerializeField] private ProductPanelCustom _productPanelTemplate;
    [SerializeField] private LayoutGroup _productCatalogLayoutGroup;
    [SerializeField] private AuthorizationButton _authorizationButton;

    private readonly List<ProductPanelCustom> _productPanels = new List<ProductPanelCustom>();
    private PurchasedProductList _purchasedProductList;
    private IAPShopButtonView _shopButtonView;
    private CatalogProduct[] _products;
    private IAPShopProductsOpener _opener;
    private int _maxGoldMultiplicator = 5;

    private void Awake()
    {
        _productPanelTemplate.gameObject.SetActive(false);
        _purchasedProductList = GetComponent<PurchasedProductList>();
        _opener = GetComponent<IAPShopProductsOpener>();
        _shopButtonView = GetComponentInChildren<IAPShopButtonView>();
    }

    private void Start()
    {
        LoadProductCatalog();
        _purchasedProductList.LoadPurchasedProducts(SetNumberOfAvailableProducts);
    }

    private void LoadProductCatalog()
    {
#if UNITY_EDITOR || TEST
        string sampleResponseJson = "{\"products\":[{\"id\":\"goldFree\",\"title\":\"Òåñòëîë\",\"description\":\"\",\"imageURI\":\"/default256x256\",\"price\":\"1 YAN\",\"priceValue\":\"1\",\"priceCurrencyCode\":\"YAN\"},{\"id\":\"levelEndFree\",\"title\":\"Æåëåøå÷êà\",\"description\":\"\",\"imageURI\":\"https://avatars.mds.yandex.net/get-games/2977039/2a0000018627c05340c1234f5ceb18517812//default256x256\",\"price\":\"4 YAN\",\"priceValue\":\"4\",\"priceCurrencyCode\":\"YAN\"}]}";
        SaveCatalogProduct(JsonUtility.FromJson<GetProductCatalogResponse>(sampleResponseJson).products);
#else
        Billing.GetProductCatalog(productCatalogReponse => SaveCatalogProduct(productCatalogReponse.products));
#endif
    }

    private void SaveCatalogProduct(CatalogProduct[] products)
    {
        _products = products;
    }

    public void UpdateProductCatalog()
    {
        foreach (ProductPanelCustom productPanel in _productPanels)
        {
            productPanel.ProductBought -= OnProductBought;
            Destroy(productPanel.gameObject);
        }

        _productPanels.Clear();

        foreach (CatalogProduct product in _products)
        {

            ProductPanelCustom productPanel = Instantiate(_productPanelTemplate, _productCatalogLayoutGroup.transform);
            _productPanels.Add(productPanel);

            productPanel.gameObject.SetActive(true);
            productPanel.Product = product;

            if (_opener.IsAbleToBuy(product))
            {
                if (productPanel.Product.id != ProductID.goldFree)
                {
                    if (_purchasedProductList.IsProductPurchased(productPanel.Product))
                        productPanel.SetSoldOutStatus();
                    else
                        productPanel.ProductBought += OnProductBought;
                }
                else
                {
                    if (_purchasedProductList.StartGoldLevel >= _maxGoldMultiplicator)
                    {
                        productPanel.SetSoldOutStatus();
                        productPanel.SetMultiplicatorText(_maxGoldMultiplicator);
                    }
                    else if (_purchasedProductList.StartGoldLevel > 0)
                    {
                        productPanel.ProductBought += OnProductBought;
                        productPanel.SetMultiplicatorText(_purchasedProductList.StartGoldLevel);
                    }
                    else
                    {
                        productPanel.ProductBought += OnProductBought;
                    }

                }
            }
            else
            {
                productPanel.SetClosedtatus(_opener.GetDemandedLevel(productPanel.Product.id));
            }

            SetNumberOfAvailableProducts();
        }
    }

    public void CheckAuthorization()
    {
        if (!PlayerAccount.IsAuthorized)
        {
            _authorizationButton.gameObject.SetActive(true);
            return;
        }
        else
        {
            _authorizationButton.gameObject.SetActive(false);
        }
    }

    private void OnProductBought(ProductPanelCustom product)
    {
        if (product != null)
            _purchasedProductList.AddNewPurchasedProduct(product.Product);

        SendMetrica(product.Product.id);

        if (product.Product.id != ProductID.goldFree)
        {
            product.ProductBought -= OnProductBought;
            product.SetSoldOutStatus();
        }
        else
        {
            if (_purchasedProductList.StartGoldLevel == _maxGoldMultiplicator)
            {
                product.SetSoldOutStatus();
                product.ProductBought -= OnProductBought;
                product.SetMultiplicatorText(_maxGoldMultiplicator);
            }
            else
            {
                product.SetMultiplicatorText(_purchasedProductList.StartGoldLevel);
            }

#if !UNITY_EDITOR
            YandexMetrica.Send($"freeGoldBuy{_purchasedProductList.StartGoldLevel}");
#endif
        }

        SetNumberOfAvailableProducts();
    }

    private void SetNumberOfAvailableProducts()
    {
        int numberOfAvailableProducts = 0;

        foreach (var product in _products)
            if (_opener.IsAbleToBuy(product))
            {
                if (product.id != ProductID.goldFree)
                {
                    if (!_purchasedProductList.IsProductPurchased(product))
                        numberOfAvailableProducts++;
                }
                else
                {
                    if (_purchasedProductList.StartGoldLevel < _maxGoldMultiplicator)
                        numberOfAvailableProducts++;
                }

            }

        _shopButtonView.SetNumberOfAvailableProducts(numberOfAvailableProducts);
    }

    private void SendMetrica(string id)
    {
#if !UNITY_EDITOR
        switch (id)
        {
            case ProductID.dragonFree:
                YandexMetrica.Send("dragonADS");
                break;
            case ProductID.tntFree:
                YandexMetrica.Send("catapultBuy");
                break;
            case ProductID.mountFree:
                YandexMetrica.Send("mountBuy");
                break;
            case ProductID.goldFree:
                YandexMetrica.Send("goldBuy");
                break;
            case ProductID.magnetFree:
                YandexMetrica.Send("magnetBuy");
                break;
            case ProductID.levelEndFree:
                YandexMetrica.Send("adsBuy");
                break;
            default:
                break;
        }
#endif
    }
}
