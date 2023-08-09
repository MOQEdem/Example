using UnityEngine;

public class GameDifficultyController : MonoBehaviour
{
    [SerializeField] private BuildableObject[] _buildings;
    [SerializeField] private UpgradeButton[] _upgrades;

    private GameDifficulty _gameDifficulty;
    private float _buildingCoefficient = 0.4f;
    private float _upgradeCoefficient = 0.3f;

    public int CurrentDifficulty => Mathf.FloorToInt(_gameDifficulty.LevelOfDifficulty);

    private void Awake()
    {
        _gameDifficulty = new GameDifficulty();
        _gameDifficulty.Load();
    }

    private void OnEnable()
    {
        foreach (var building in _buildings)
            building.Builded += OnBuildingBuilded;

        foreach (var upgrades in _upgrades)
            upgrades.Upgraded += OnUpgradeBought;
    }

    private void OnDisable()
    {
        foreach (var building in _buildings)
            building.Builded -= OnBuildingBuilded;

        foreach (var upgrades in _upgrades)
            upgrades.Upgraded -= OnUpgradeBought;
    }

    private void OnBuildingBuilded()
    {
        _gameDifficulty.AddDifficulty(_buildingCoefficient);
        _gameDifficulty.Save();
        Debug.Log(_gameDifficulty.LevelOfDifficulty);
    }

    private void OnUpgradeBought()
    {
        _gameDifficulty.AddDifficulty(_upgradeCoefficient);
        _gameDifficulty.Save();
    }
}
