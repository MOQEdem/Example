using System.Linq;
using UnityEngine;

public class DragonFireAttack : MonoBehaviour
{
    [SerializeField] private Transform _dragonFireSpawnPoint;
    [SerializeField] private DragonFireMover[] _firesPool;

    public void HitTarget(Vector3 target)
    {
        DragonFireMover fire = _firesPool.FirstOrDefault(f => f.isActiveAndEnabled == false);
        if (fire == null) 
            return;
        fire.transform.position = _dragonFireSpawnPoint.position;
        fire.MoveTo(target);
    }
}
