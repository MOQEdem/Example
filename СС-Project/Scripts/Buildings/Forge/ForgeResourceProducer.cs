using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class ForgeResourceProducer : MonoBehaviour
{
    [SerializeField] private BuffZone _zone;
    [SerializeField] private ForgeRecource _resourcePrefab;
    [SerializeField] private GameObject[] _tablePoints;
    [SerializeField] private GameObject[] _poolPoints;
    [SerializeField] private GameObject _finalPoints;

    private ForgeSound _sound;
    private Coroutine _producingResource;
    private bool _isActive = true;
    private float _animationTime = 1f;

    public event Action<ForgeRecource> ResourceProduced;

    private void Awake()
    {
        _sound = GetComponent<ForgeSound>();
    }

    private void OnEnable()
    {
        _zone.ChangeCount += OnChangeCount;
    }

    private void OnDisable()
    {
        _zone.ChangeCount -= OnChangeCount;
    }

    private void OnChangeCount()
    {
        if (_producingResource == null && _zone.Resources.Count > 0)
        {
            _producingResource = StartCoroutine(ProducingResource());
            _zone.ChangeCount -= OnChangeCount;
        }
    }

    public void SetProducingStepTime(float time)
    {
        _animationTime = time;
    }

    public void SetResourcePrefab(ForgeRecource resource)
    {
        _resourcePrefab = resource;
    }

    public void SetActivity(bool isActive)
    {
        _isActive = isActive;

        if (!isActive && _producingResource != null)
        {
            StopCoroutine(_producingResource);
            _producingResource = null;
        }

        if (isActive && _zone.Resources.Count > 0)
        {
            if (_producingResource == null)
                _producingResource = StartCoroutine(ProducingResource());
        }
    }

    private IEnumerator ProducingResource()
    {
        var delay = new WaitForSeconds(_animationTime);

        while (_zone.Resources.Count > 0 && _isActive)
        {
            var currentResource = _zone.SpendResource();

            if (currentResource != null)
            {
                currentResource.SetTarget(_tablePoints[0].transform.position);
                yield return delay;

                ForgeRecource forgeRecource = Instantiate(_resourcePrefab, _tablePoints[0].transform);
                forgeRecource.transform.parent = null;

                currentResource.transform.DOKill();
                Destroy(currentResource.gameObject);

                _sound.PlayWarmingSound();

                forgeRecource.StartSetHotMaterial(_animationTime);
                Tween move = forgeRecource.transform.DOMove(_tablePoints[1].transform.position, _animationTime);
                yield return move.WaitForCompletion();

                move = forgeRecource.transform.DOJump(_poolPoints[0].transform.position, 1, 1, _animationTime / 2);
                yield return move.WaitForCompletion();

                forgeRecource.SetSword();
                _sound.PlayHitSound();

                move = forgeRecource.transform.DOJump(_poolPoints[1].transform.position, 1, 1, _animationTime / 2);
                yield return move.WaitForCompletion();

                _sound.PlayHitSound();

                move = forgeRecource.transform.DOJump(_finalPoints.transform.position, 1, 1, _animationTime / 2);
                yield return move.WaitForCompletion();

                _sound.PlayWorkSound();

                forgeRecource.Pack(_animationTime);
                yield return delay;

                ResourceProduced?.Invoke(forgeRecource);
            }
        }

        _zone.ChangeCount += OnChangeCount;
        _producingResource = null;
    }
}
