using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessToFreeADS : MonoBehaviour
{
    [SerializeField] private GoldMagnet _magnetGiver;
    [SerializeField] private ResourceCollectorSwitcher _switcher;
    [SerializeField] private RewardChest _reward;
    [SerializeField] private StartResource _startResource;

    private List<AccessType> _accessTypes = new List<AccessType>();
    private int _numberOfGoldPacks = 0;

    public void AddAccessToADS(CatalogProduct newProduct)
    {
        AddAccess(newProduct.id);
    }

    public void AddAccessToMultipleADS(CatalogProduct[] products)
    {
        foreach (CatalogProduct product in products)
            AddAccess(product.id);

        TryActivateStartGold();
        TryActivateMagnet();
    }

    public bool IsPlayerHaveAccess(AccessType type)
    {
        foreach (AccessType accessType in _accessTypes)
            if (accessType == type)
                return true;

        return false;
    }

    private void AddAccess(string productID)
    {
        switch (productID)
        {
            case ProductID.dragonFree:
                _accessTypes.Add(AccessType.Dragon);
                break;
            case ProductID.tntFree:
                _accessTypes.Add(AccessType.TNT);
                break;
            case ProductID.mountFree:
                _accessTypes.Add(AccessType.Mount);
                break;
            case ProductID.goldFree:
                _accessTypes.Add(AccessType.Gold);
                break;
            case ProductID.levelEndFree:
                _accessTypes.Add(AccessType.LevelFinish);
                break;
            case ProductID.magnetFree:
                _accessTypes.Add(AccessType.Magnet);
                break;
            default:
                break;
        }
    }

    private void TryActivateStartGold()
    {
        foreach (AccessType accessType in _accessTypes)
        {
            if (accessType == AccessType.Gold)
            {
                _numberOfGoldPacks++;
            }
        }

        if (_numberOfGoldPacks > 0)
        {
            _startResource.SetStartGold(_numberOfGoldPacks);
            Destroy(_reward.gameObject);
        }
    }

    private void TryActivateMagnet()
    {
        foreach (AccessType accessType in _accessTypes)
            if (accessType == AccessType.Magnet)
            {
                _switcher.SwitchCollectors();
                Destroy(_magnetGiver.gameObject);
            }
    }
}

public enum AccessType
{
    Dragon,
    TNT,
    Mount,
    Gold,
    LevelFinish,
    Magnet
}

public class ProductID
{
    public const string dragonFree = nameof(dragonFree);
    public const string tntFree = nameof(tntFree);
    public const string mountFree = nameof(mountFree);
    public const string goldFree = nameof(goldFree);
    public const string levelEndFree = nameof(levelEndFree);
    public const string magnetFree = nameof(magnetFree);
}
