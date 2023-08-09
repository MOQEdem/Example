using UnityEngine;
using DG.Tweening;


public class SizeChangedAnimation : MonoBehaviour
{
    [SerializeField] private float _sizeMultiplier = 1.1f;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private int _loopCount = 1;

    public void StartChangeSize()
    {
        float targetSize = transform.localScale.x * _sizeMultiplier;
        transform.DOScale(targetSize, _duration).SetLoops(_loopCount, LoopType.Yoyo).SetEase(Ease.Linear);
    }
   
}
