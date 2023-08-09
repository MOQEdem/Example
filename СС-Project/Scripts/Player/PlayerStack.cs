using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerStack : MonoBehaviour
{
    [Header("Capcity")]
    [SerializeField] private int _capacity = 100;
    [Space]
    [Header("VisualSettings")]
    [SerializeField] private Transform _startPointPlayerResource;
    [SerializeField] private Transform _startPointEnemyResource;
    [SerializeField] private float _stepOffset;

    private PlayerMover _mover;
    private List<Resource> _waitList = new List<Resource>();
    private Coroutine _collectingResources;

    public List<Resource> PlayerResource { get; private set; } = new List<Resource>();
    public List<Resource> EnemyResource { get; private set; } = new List<Resource>();
    public bool IsReadyToTransit => _mover.InputDirection == Vector2.zero;
    public bool IsFull => (PlayerResource.Count + EnemyResource.Count + _waitList.Count) >= _capacity;
    public int Capacity => _capacity;

    public event Action ResourceQuantityChanged;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
    }

    public void AddResourceToWaitList(Resource resource)
    {
        resource.transform.DOKill();

        _waitList.Add(resource);

        if (_collectingResources == null)
            _collectingResources = StartCoroutine(CollectingResources());
    }

    public void SetActivity(bool isActive)
    {
        _startPointEnemyResource.gameObject.SetActive(isActive);
        _startPointPlayerResource.gameObject.SetActive(isActive);
    }

    public void UpgradeCapacity(int newCapacity)
    {
        if (newCapacity > _capacity)
            _capacity = newCapacity;
    }

    private void ApplyResource(Resource resource)
    {
        if (resource.ResourceType == ResourceType.Enemy)
            AddResource(resource, EnemyResource, _startPointEnemyResource);
        else
            AddResource(resource, PlayerResource, _startPointPlayerResource);

        ResourceQuantityChanged?.Invoke();
    }

    private void AddResource(Resource resource, List<Resource> resources, Transform stackPoint)
    {
        resources.Add(resource);
        resource.SetParent(stackPoint);
        resource.SetTarget(GetCurrentFreePoint(resources));

        if (resource.ResourceType == ResourceType.Enemy)
        {
            SetPlayerResourcePosition();
        }
    }

    public Resource Transit(List<Resource> resources)
    {
        if (resources.Count > 0)
        {
            int numberResource = resources.Count - 1;
            Resource currentResource = resources[numberResource];
            resources.RemoveAt(numberResource);
            ResourceQuantityChanged?.Invoke();

            if (currentResource.ResourceType == ResourceType.Enemy)
            {
                SetPlayerResourcePosition();
            }

            return currentResource;
        }

        return null;
    }

    private void SetPlayerResourcePosition()
    {
        Vector3 targetPosition = GetCurrentFreePoint(EnemyResource);
        Vector3 currentPosition = _startPointPlayerResource.transform.localPosition;

        targetPosition.x = currentPosition.x;
        targetPosition.z = currentPosition.z;
        targetPosition.y += _startPointEnemyResource.transform.localPosition.y;

        _startPointPlayerResource.transform.DOLocalMove(targetPosition, 0.3f);
    }

    private Vector3 GetCurrentFreePoint(List<Resource> capsuls)
    {
        return new Vector3(0, capsuls.Count * _stepOffset, 0);
    }

    public void ReplaceResources(Player fromPlayer, Player targetPlayer)
    {
        foreach (var resource in fromPlayer.PlayerStack.PlayerResource)
        {
            targetPlayer.PlayerStack.PlayerResource.Add(resource);
            resource.SetParent(targetPlayer.PlayerStack._startPointPlayerResource);
            resource.SetTarget(GetCurrentFreePoint(targetPlayer.PlayerStack.PlayerResource));
        }

        foreach (var resource in fromPlayer.PlayerStack.EnemyResource)
        {
            targetPlayer.PlayerStack.EnemyResource.Add(resource);
            resource.SetParent(targetPlayer.PlayerStack._startPointEnemyResource);
            resource.SetTarget(GetCurrentFreePoint(targetPlayer.PlayerStack.EnemyResource));
        }

        fromPlayer.PlayerStack.PlayerResource.Clear();
        fromPlayer.PlayerStack.EnemyResource.Clear();
    }

    private IEnumerator CollectingResources()
    {
        var delay = new WaitForSeconds(0.05f);

        while (_waitList.Count > 0)
        {
            yield return delay;

            var resource = _waitList[0];
            ApplyResource(resource);
            _waitList.Remove(resource);

        }

        _collectingResources = null;
    }
}
