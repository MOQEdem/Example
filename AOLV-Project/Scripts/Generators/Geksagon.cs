using System.Collections;
using UnityEngine;

public class Geksagon : MonoBehaviour
{
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private EndPoint[] _endPoints;
    [SerializeField] private UnitSpawnPoint[] _resourcesSpawnPointsLeft;
    [SerializeField] private UnitSpawnPoint[] _resourcesSpawnPointsRight;
    [SerializeField] private UnitSpawnPoint[] _enemiesSpawnPoints;

    private Unit[] _resources;
    private Unit[] _enemies;
    private Coroutine _spawningResources;
    private Coroutine _spawningEnemies;

    public StartPoint StartPoint => _startPoint;
    public EndPoint[] EndPoints => _endPoints;

    public void SpawnUnit(Unit[] resources, Unit[] enemies, Player player)
    {
        _resources = resources;
        _enemies = enemies;

        int random = Random.Range(0, 2);

        if (random == 0)
            _spawningResources = StartCoroutine(SpawningUnits(_resourcesSpawnPointsLeft, _resources, player));
        else
            _spawningResources = StartCoroutine(SpawningUnits(_resourcesSpawnPointsRight, _resources, player));

        _spawningEnemies = StartCoroutine(SpawningUnits(_enemiesSpawnPoints, _enemies, player));
    }

    public void SpawnOnlyEnemies(Unit[] enemies)
    {
        _enemies = enemies;

        // _spawningEnemies = StartCoroutine(SpawningUnits(_enemiesSpawnPoints, _enemies, player));
        //_spawningResources = StartCoroutine(SpawningUnits(_enemiesSpawnPoints, _enemies, player));
    }

    private IEnumerator SpawningUnits(UnitSpawnPoint[] points, Unit[] units, Player player)
    {
        float spawningDelayTime = 0.5f;

        var spawningDelay = new WaitForSeconds(spawningDelayTime);

        for (int i = 0; i < points.Length; i++)
        {
            points[i].PlayParticle();

            int spawningUnitNumber = Random.Range(0, units.Length);

            var unit = Instantiate(units[spawningUnitNumber], points[i].transform.position, points[i].transform.rotation);

            if (unit is Enemy enemy)
            {
                enemy.SetPlayer(player);
            }

            //points[i].GrowUnitSize(unit);
        }

        yield return null;
    }
}
