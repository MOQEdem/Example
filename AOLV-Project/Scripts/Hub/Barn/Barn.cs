using UnityEngine;

public class Barn : MonoBehaviour
{
    [SerializeField] private SpawnedResource _prefab;
    [SerializeField] private Transform[] _spawnPoints;

    private int _maxResourcesSpawn = 3;

    private void OnEnable()
    {
        for (int i = 0; i < _maxResourcesSpawn; i++)
        {
            Instantiate(_prefab, _spawnPoints[i]);
        }
    }
}
