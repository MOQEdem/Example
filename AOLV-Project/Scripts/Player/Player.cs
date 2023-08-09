using UnityEngine;

public class Player : Unit
{
    [SerializeField] private Timer _timer;
    [SerializeField] private PlayerAttacker _playerAttacker;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ResourceBag _hubResources;

    public ResourceBag HubResources => _hubResources;
    public Timer Timer => _timer;
    public Joystick Joystick => _joystick;

    public void TakeNewWeapon(WeaponUpgrade upgrade)
    {
        _playerAttacker.UpgradeWeapon(upgrade);
    }

    protected override void Die()
    {
    }

    protected override UnitType GetUnitType()
    {
        return UnitType.Player;
    }

    protected override void TakeDamage(AttackData attackData)
    {
        ReduceHealth(attackData.Weapon);
        CallEventHited(attackData);
    }
}
