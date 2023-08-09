using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IAPShopIconGiver", menuName = "GameAssets/IAPShopIconGiver")]
public class IAPShopIconGiver : ScriptableObject
{
    [SerializeField] private IconData[] _data;

    private const string LocalizationLanguage = nameof(Localization);

    public Texture GetTexture(string productID)
    {
        foreach (IconData data in _data)
            if (data.ProductID == productID)
                return data.Icon;

        return null;
    }

    public string GetTitle(string productID)
    {
        foreach (IconData data in _data)
        {
            if (data.ProductID == productID)
            {
                switch (PlayerPrefs.GetString(LocalizationLanguage))
                {
                    case Language.Russian:
                        return data.RussianText;
                    case Language.English:
                        return data.EnglishText;
                    case Language.Turkish:
                        return data.TurkishText;
                    default:
                        return data.RussianText;
                }
            }
        }

        return null;
    }
}

[Serializable]
public class IconData
{
    [SerializeField] private string _productID;
    [SerializeField] private Texture2D _icon;
    [SerializeField] private string _russianText;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;

    public string ProductID => _productID;
    public Texture Icon => _icon;
    public string RussianText => _russianText;
    public string EnglishText => _englishText;
    public string TurkishText => _turkishText;
}