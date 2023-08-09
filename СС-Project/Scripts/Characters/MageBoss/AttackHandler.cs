using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField] private MeteorAttack _meteorAttack;
    [SerializeField] private MageBossRange _mageBossRange;

    private void HandleMeteorAttack() => 
        _meteorAttack.Cast();

    private void HandleCommonAttack() => 
        _mageBossRange.Shoot();
}
