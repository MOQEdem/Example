using UnityEngine;
using DG.Tweening;

public class StatuePart : MonoBehaviour
{
    private MeshRenderer _renderer;

    private bool _isActivated = false;
    private Vector3 _startPosition;
    private Vector3 _startRoration;
    private Vector3 _startScale;
    private float _animationTime = 1f;
    private int _positionRandomizerRange = 6;

    public bool IsActivated => _isActivated;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();

        _startPosition = transform.position;
        _startRoration = transform.eulerAngles;
        _startScale = transform.localScale;
        _renderer.enabled = false;
    }

    public void ActivateAtStart()
    {
        _isActivated = true;
        _renderer.enabled = true;
    }

    public void Activate()
    {
        _isActivated = true;

        transform.localScale = Vector3.zero;
        transform.position = (Random.insideUnitSphere * _positionRandomizerRange) + _startPosition;

        Vector3 randomRotation = new Vector3(Random.Range(0, 361), Random.Range(0, 361), Random.Range(0, 361));
        transform.rotation = Quaternion.Euler(randomRotation);

        _renderer.enabled = true;

        transform.DOScale(_startScale, _animationTime / 2);
        transform.DORotate(_startRoration, _animationTime);
        transform.DOMove(_startPosition, _animationTime + 1);
    }
}
