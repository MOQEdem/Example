using UnityEngine;
using DG.Tweening;

public class MaterialColorSwitcher : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Color _targetColor = Color.white;
    [SerializeField] private float _transitionDuration = 0.1f;
    [SerializeField] private int _blinkCount = 4;

    private Material _material;
    private Color _startColor;

    public void Init()
    {
        _material = _meshRenderer.material;
        _startColor = _material.color;
    }

    public void Blink()
    {
        _material.DOColor(_targetColor, _transitionDuration).SetLoops(_blinkCount, LoopType.Yoyo);
    }

    public void SetDieColor()
    {
        _material.DOColor(_targetColor, _transitionDuration);
    }
}
