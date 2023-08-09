using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

public class PurchasedProductList : MonoBehaviour
{
    [SerializeField] private PlayerAccessToFreeADS _playerAccess;
    [SerializeField] private CloadSave _cloadSave;

    private CatalogProduct[] _purchasedProducts;
    private int _startGoldLevel = 0;


    private static Action _onPurchasedProductsLoadCallback;

    public int StartGoldLevel => _startGoldLevel;

    public void LoadPurchasedProducts(Action onSuccessCallback)
    {
        _onPurchasedProductsLoadCallback = onSuccessCallback;

#if UNITY_EDITOR || TEST
        UpdatePurchasedProductsList(JsonUtility.FromJson<GetProductCatalogResponse>("{\"products\":[{\"id\":\"goldFree\",\"title\":\"Тестлол\",\"description\":\"\",\"imageURI\":\"/default256x256\",\"price\":\"1 YAN\",\"priceValue\":\"1\",\"priceCurrencyCode\":\"YAN\"}, {\"id\":\"goldFree\",\"title\":\"Тестлол\",\"description\":\"\",\"imageURI\":\"/default256x256\",\"price\":\"1 YAN\",\"priceValue\":\"1\",\"priceCurrencyCode\":\"YAN\"}, {\"id\":\"goldFree\",\"title\":\"Тестлол\",\"description\":\"\",\"imageURI\":\"/default256x256\",\"price\":\"1 YAN\",\"priceValue\":\"1\",\"priceCurrencyCode\":\"YAN\"}]}").products);
#else
        StartCoroutine(WaitingToLoad());
#endif
    }

    public bool IsProductPurchased(CatalogProduct product)
    {
        foreach (CatalogProduct purchasedProduct in _purchasedProducts)
        {
            if (purchasedProduct.id == product.id)
            {
                return true;
            }
        }

        return false;
    }

    public void AddNewPurchasedProduct(CatalogProduct newProduct)
    {
        CatalogProduct[] newPurchasedProducts = new CatalogProduct[_purchasedProducts.Length + 1];

        for (int i = 0; i < _purchasedProducts.Length; i++)
        {
            newPurchasedProducts[i] = _purchasedProducts[i];
        }

        newPurchasedProducts[newPurchasedProducts.Length - 1] = newProduct;

        _purchasedProducts = newPurchasedProducts;

        _playerAccess.AddAccessToADS(newProduct);

#if !UNITY_EDITOR && YANDEX_GAMES
        PurchasedProductsToSave purchasedProductsToSave = new PurchasedProductsToSave();

        purchasedProductsToSave.PurchasedProducts = _purchasedProducts;
        var jsonString = JsonUtility.ToJson(purchasedProductsToSave);

        _cloadSave.SetSavedData(SaveID.purchasedProduct, jsonString);
#endif

        if (newProduct.id == ProductID.goldFree)
            _startGoldLevel++;
    }

    private void UpdatePurchasedProductsList(CatalogProduct[] purchasedProducts)
    {
        _purchasedProducts = purchasedProducts;
        _playerAccess.AddAccessToMultipleADS(purchasedProducts);

        foreach (CatalogProduct product in _purchasedProducts)
        {
            if (product.id == ProductID.goldFree)
            {
                _startGoldLevel++;
            }
        }

        _onPurchasedProductsLoadCallback?.Invoke();
    }

    private IEnumerator WaitingToLoad()
    {
        while (!_cloadSave.IsLoaded)
            yield return null;

        UpdatePurchasedProductsList(JsonUtility.FromJson<PurchasedProductsToSave>(_cloadSave.GetSavedData(SaveID.purchasedProduct)).PurchasedProducts);
    }

    [Serializable]
    public class PurchasedProductsToSave
    {
        [field: Preserve]
        [SerializeField]
        public CatalogProduct[] PurchasedProducts;
    }
}
