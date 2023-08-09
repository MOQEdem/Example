using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private IslandGeneratorData _data;
    [SerializeField] private GameDifficultyController _gameDifficultyController;
    [SerializeField] private Geksagon _startGeksagon;
    [SerializeField] private Geksagon _geksagonPrefab;
    [SerializeField] private Geksagon _rockPrefab;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private Pier _pier;
    [SerializeField] private SceneLoadController _sceneLoadController;

    private Unit[] _resources;
    private Unit[] _enemies;
    private int _mapSize;
    private Geksagon _currentGeksagon;

    private void Start()
    {
        TakeGameData();

        _currentGeksagon = _startGeksagon;

        int pointNumber = Random.Range(0, _currentGeksagon.EndPoints.Length);
        int previousPointNumber = -1;

        for (int i = 0; i < _mapSize; i++)
        {
            while (pointNumber == previousPointNumber)
            {
                pointNumber = Random.Range(0, _currentGeksagon.EndPoints.Length);
            }

            Geksagon newGeksagon = SpawnGeksagon(pointNumber);

            int randomazer = Random.Range(0, 5);

            if (randomazer < 3 && i < _mapSize)
            {
                for (int j = pointNumber + 1; j < _currentGeksagon.EndPoints.Length; j++)
                {
                    if (!_currentGeksagon.EndPoints[j].IsTaken && i < _mapSize)
                    {
                        SpawnGeksagon(j);

                        i++;
                    }
                }
            }
            else
            {
                for (int j = pointNumber + 1; j < _currentGeksagon.EndPoints.Length; j++)
                {
                    if (!_currentGeksagon.EndPoints[j].IsTaken)
                    {
                        Instantiate(_rockPrefab, _currentGeksagon.EndPoints[j].transform.position, _currentGeksagon.EndPoints[j].transform.rotation);
                    }
                }

            }

            previousPointNumber = pointNumber;

            _currentGeksagon = newGeksagon;
        }

        //  var pier = Instantiate(_pier, _currentGeksagon.EndPoints[1].transform.position, _currentGeksagon.EndPoints[1].transform.rotation);
        // _sceneLoadController.AddLoadTrigger(pier);

        _surface.BuildNavMesh();
    }

    private Geksagon SpawnGeksagon(int pointIndex)
    {
        EndPoint spawnPoint = _currentGeksagon.EndPoints[pointIndex];

        spawnPoint.Take();

        var newGeksagon = Instantiate(_geksagonPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        Vector3 newGeksagonScale = new Vector3(newGeksagon.transform.localScale.x, Random.Range(1f, 2f), newGeksagon.transform.localScale.z);

        newGeksagon.transform.localScale = newGeksagonScale;

        newGeksagon.SpawnUnit(_resources, _enemies, player);

        return newGeksagon;
    }

    private void TakeGameData()
    {
        GameDifficultyData gameDifficultyData = _data.GetGameDifficultySettings(_gameDifficultyController.CurrentDifficulty);

        _resources = gameDifficultyData.Resources;
        _enemies = gameDifficultyData.Enemies;
        _mapSize = gameDifficultyData.MapSize;
    }
}
