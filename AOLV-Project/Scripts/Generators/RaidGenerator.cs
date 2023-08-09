using UnityEngine;
using UnityEngine.AI;

public class RaidGenerator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private RaidGeneratorData _data;
    [SerializeField] private GameDifficultyController _gameDifficultyController;
    [SerializeField] private Geksagon _startGeksagon;
    [SerializeField] private RaidMap[] _raidMapPrefabs;
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private Pier _pier;
    [SerializeField] private SceneLoadController _sceneLoadController;

    private Unit[] _resources;
    private Unit[] _objects;
    private Unit[] _enemies;

    private void Start()
    {
        TakeGameData();

        int pointNumber = Random.Range(0, _startGeksagon.EndPoints.Length);

        SpawnRaidMap(pointNumber);

        _surface.BuildNavMesh();
    }

    private RaidMap SpawnRaidMap(int pointIndex)
    {
        EndPoint spawnPoint = _startGeksagon.EndPoints[pointIndex];

        int mapPrefabeNumber = Random.Range(0, _raidMapPrefabs.Length);

        var map = Instantiate(_raidMapPrefabs[mapPrefabeNumber], spawnPoint.transform.position, spawnPoint.transform.rotation);

        map.SpawnUnit(_resources, _enemies, _objects, player);

        return map;
    }

    private void TakeGameData()
    {
        RaidGameDifficultyData gameDifficultyData = _data.GetGameDifficultySettings(_gameDifficultyController.CurrentDifficulty);

        _resources = gameDifficultyData.Resources;
        _enemies = gameDifficultyData.Enemies;
        _objects = gameDifficultyData.Objects;
    }
}
