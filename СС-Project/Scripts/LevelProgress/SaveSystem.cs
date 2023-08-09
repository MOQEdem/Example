using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    private CharacterProduserUpgrader[] _characterProduserUpgraders;
    private ForgeResourceUpgrader[] _forgeResourceUpgraders;
    private ForgeTimeUpgrader[] _forgeTimeUpgraders;
    private InteractiveZoneProgressLoader[] _interactiveZoneProgressLoaders;
    private UpgradeZone _upgradeZone;
    private PlayerStatsManager _playerStatsManager;
    private PlayerUpgrader _playerUpgrader;
    private StatueBuilder _statueBuildingZone;
    private Button _button;
    private StorageUpgrader _storageUpgrader;

    private void Awake()
    {
        _button = GetComponentInChildren<WinButton>(true).GetComponent<Button>();
        _playerUpgrader = GetComponentInChildren<PlayerUpgrader>();
        _playerStatsManager = GetComponentInChildren<PlayerStatsManager>(true);
        _upgradeZone = GetComponentInChildren<UpgradeZone>(true);
        _characterProduserUpgraders = GetComponentsInChildren<CharacterProduserUpgrader>(true);
        _forgeResourceUpgraders = GetComponentsInChildren<ForgeResourceUpgrader>(true);
        _forgeTimeUpgraders = GetComponentsInChildren<ForgeTimeUpgrader>(true);
        _interactiveZoneProgressLoaders = GetComponentsInChildren<InteractiveZoneProgressLoader>(true);
        _statueBuildingZone = GetComponentInChildren<StatueBuilder>(true);
        _storageUpgrader = GetComponentInChildren<StorageUpgrader>(true);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SaveAll);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SaveAll);
    }

    private void SaveAll()
    {
        foreach (var upgrader in _characterProduserUpgraders)
            upgrader.Save();

        foreach (var upgrader in _forgeResourceUpgraders)
            upgrader.Save();

        foreach (var upgrader in _forgeTimeUpgraders)
            upgrader.Save();

        foreach (var loader in _interactiveZoneProgressLoaders)
            loader.SaveValue();

        if (_upgradeZone != null)
            _upgradeZone.Save();

        if (_statueBuildingZone != null)
            _statueBuildingZone.Save();

        if (_playerStatsManager != null)
            _playerStatsManager.Save();

        if (_storageUpgrader != null)
            _storageUpgrader.Save();

        if (_playerUpgrader != null)
            _playerUpgrader.Save();
    }
}
