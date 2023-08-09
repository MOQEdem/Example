using UnityEngine;

public class DragonBossAttackHandler : MonoBehaviour
{
    [SerializeField] private DragonBoss _dragonBoss;
    
    private void HandleFireballAttack() => 
        _dragonBoss.SpawnFireBall();

    private void HandleMeleeAttack() =>
        _dragonBoss.MeleeAttack();
}
