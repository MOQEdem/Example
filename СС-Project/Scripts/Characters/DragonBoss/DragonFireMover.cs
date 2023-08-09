using DG.Tweening;
using UnityEngine;

public class DragonFireMover : MonoBehaviour
{
    [SerializeField] private DragonFireDamage _dragonFireDamage;
    [SerializeField] [Min(0)] private float _moveDuration = 1;
    public void MoveTo(Vector3 target)
    {
        gameObject.SetActive(true);
        transform.DOMove(target, _moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _dragonFireDamage.Explode())
            .OnKill(()=> gameObject.SetActive(false));
    }
}
