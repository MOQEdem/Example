using UnityEngine;

public class AxeUpgradeButton : UpgradeButton
{
    [SerializeField] private WeaponUpgrades _upgrades;

    protected override bool IsComplete() => CurrentStep >= _upgrades.UpgradesCount;

    protected override bool IsEnoughResources() => Player.HubResources.IsAbleToSpendResource(new ResourcePack(_upgrades.UpgradesBook[CurrentStep].Cost, _upgrades.UpgradesBook[CurrentStep].ResourceType));

    protected override string SaveID() => nameof(AxeUpgradeButton);

    protected override ResourcePack UpgradeCost() => new ResourcePack(_upgrades.UpgradesBook[CurrentStep].Cost, _upgrades.UpgradesBook[CurrentStep].ResourceType);

    protected override void Start()
    {
        Player.TakeNewWeapon(_upgrades.UpgradesBook[CurrentStep]);

        CurrentStep++;

        base.Start();
    }

    protected override void OnButtonClicked()
    {
        Debug.Log(_upgrades.UpgradesBook[CurrentStep].Weapon.name);

        Player.TakeNewWeapon(_upgrades.UpgradesBook[CurrentStep]);

        TakeCost();

        PlayerPrefs.SetInt(SaveID(), CurrentStep);

        SendAction();

        CurrentStep++;

        UpdateCostView();

        CheckUpgradePossibility();
    }
}
