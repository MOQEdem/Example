using System;
using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourceType _spawnedResourceType;
    [SerializeField] private ResourcesData _data;
    [SerializeField] private int _oneTimeSpawnModelCount;

    private ResourceBag _bag;
    private ResourceModel _spawnedResource;
    private float _spawnDelay = 0.1f;

    private Coroutine _spendingResources;
    private Coroutine _takingResources;

    private void Awake()
    {
        _spawnedResource = GetResourceModel(_spawnedResourceType);
        _bag = GetComponentInParent<ResourceBag>();
    }

    private void OnEnable()
    {
        _bag.ResourceSpended += OnResourceSpended;
        _bag.ResourceTaken += OnResourceTaken;
    }

    private void OnDisable()
    {
        _bag.ResourceSpended -= OnResourceSpended;
        _bag.ResourceTaken -= OnResourceTaken;

    }

    private void OnResourceSpended(ResourcePack resourcePack, Transform targetPoint)
    {
        if (_spawnedResourceType != resourcePack.Type)
            return;


        if (_spendingResources == null)
        {
            if (_bag.InSpendingProcess)
                _spendingResources = StartCoroutine(SpawningResourcesFlow(this.transform, targetPoint));
            else
                _spendingResources = StartCoroutine(SpawningResourcesOnce(resourcePack.Value, this.transform, targetPoint));
        }
    }

    private void OnResourceTaken(ResourcePack resourcePack, Transform startPoint)
    {
        if (_spawnedResourceType != resourcePack.Type)
            return;

        if (_takingResources == null)
        {
            _takingResources = StartCoroutine(SpawningResourcesOnce(resourcePack.Value, startPoint, this.transform));
        }
    }

    private IEnumerator SpawningResourcesOnce(int iterationCount, Transform startPoint, Transform pointToMove)
    {
        int numberOfModelToSpawn = iterationCount;

        if (numberOfModelToSpawn > _oneTimeSpawnModelCount)
            numberOfModelToSpawn = _oneTimeSpawnModelCount;

        int spawnedModelCount = 0;

        while (spawnedModelCount < numberOfModelToSpawn)
        {
            InstantiateModel(startPoint, pointToMove, true);

            spawnedModelCount++;
        }
        yield return null;
        _takingResources = null;
        _spendingResources = null;
    }

    private IEnumerator SpawningResourcesFlow(Transform startPoint, Transform pointToMove)
    {
        var delay = new WaitForSeconds(_spawnDelay);

        while (_bag.InSpendingProcess)
        {
            InstantiateModel(startPoint, pointToMove, false);

            yield return delay;
        }

        _takingResources = null;
        _spendingResources = null;

    }

    private void InstantiateModel(Transform startPoint, Transform pointToMove, bool isExplosionAnimation)
    {
        ResourceModel spawnedModel = Instantiate(_spawnedResource, startPoint.position, Quaternion.identity);

        spawnedModel.StartSpendingAnimation(pointToMove, isExplosionAnimation);
    }

    private ResourceModel GetResourceModel(ResourceType resourceType)
    {
        foreach (var template in _data.Data)
        {
            if (template.Type == resourceType)
            {
                return template.Model;
            }
        }

        return null;
    }
}