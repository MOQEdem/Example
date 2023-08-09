using DG.Tweening;
using UnityEngine;

public class NetDestroyTimer : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _timeToDestroy = 60f;

    private void Start() =>
        DestroyWithDelay();

    private void DestroyWithDelay()
    {
        DOTween.Sequence()
            .PrependInterval(_timeToDestroy)
            .Append(transform.DOMoveY(-2, 1))
            .OnComplete(() => Destroy(gameObject));
    }
}
