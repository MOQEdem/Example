using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class IAPShopProductsOpener : MonoBehaviour
{
    [SerializeField] private BuildingOpeningRequirements _requirements;

    private BuildingTypes[] _types = new BuildingTypes[] {
        BuildingTypes.DragonLairBuilding,
        BuildingTypes.Catapult,
        BuildingTypes.Stable,
        BuildingTypes.RewardChest,
        BuildingTypes.GoldMagnet
    };

    public List<string> AvailableID { get; private set; }

    private void Awake()
    {
        AvailableID = new List<string>();

        AvailableID.Add(ProductID.levelEndFree);

        List<BuildingData> buildingDatas = _requirements.GetListOfAvailableBuildings();

        foreach (BuildingData buildingData in buildingDatas)
            foreach (var type in _types)
                if (buildingData.Type == type)
                    AddAvailableProduct(type);
    }

    private void AddAvailableProduct(BuildingTypes type)
    {
        switch (type)
        {
            case BuildingTypes.DragonLairBuilding:
                AvailableID.Add(ProductID.dragonFree);
                break;
            case BuildingTypes.Catapult:
                AvailableID.Add(ProductID.tntFree);
                break;
            case BuildingTypes.Stable:
                AvailableID.Add(ProductID.mountFree);
                break;
            case BuildingTypes.RewardChest:
                AvailableID.Add(ProductID.goldFree);
                break;
            case BuildingTypes.GoldMagnet:
                AvailableID.Add(ProductID.magnetFree);
                break;
            default:
                break;
        }
    }

    private BuildingTypes GetBuildingType(string productID)
    {
        switch (productID)
        {
            case ProductID.dragonFree:
                return BuildingTypes.DragonLairBuilding;
            case ProductID.tntFree:
                return BuildingTypes.Catapult;
            case ProductID.mountFree:
                return BuildingTypes.Stable;
            case ProductID.goldFree:
                return BuildingTypes.RewardChest;
            case ProductID.magnetFree:
                return BuildingTypes.GoldMagnet;
            default:
                return 0;
        }
    }

    public bool IsAbleToBuy(CatalogProduct product)
    {
        foreach (var id in AvailableID)
            if (id == product.id)
                return true;

        return false;
    }

    public int GetDemandedLevel(string productID)
    {
        return _requirements.GetDemandedLevel(GetBuildingType(productID));
    }
}
