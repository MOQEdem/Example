using UnityEngine;

public class ResourceModel : MonoBehaviour
{
    [SerializeField] private ResourceAnimation _takingAnimation;

    private void OnEnable()
    {
        _takingAnimation.Completed += OnAnimationCompleted;
    }

    private void OnDisable()
    {
        _takingAnimation.Completed -= OnAnimationCompleted;
    }

    public void StartSpendingAnimation(Transform targetPosition, bool isExplosionAnimation)
    {
        _takingAnimation.TryStartAnimation(targetPosition, isExplosionAnimation);
    }

    private void OnAnimationCompleted()
    {
        Destroy(this.gameObject);
    }
}