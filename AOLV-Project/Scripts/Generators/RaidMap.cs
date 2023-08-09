using System.Collections;
using UnityEngine;

public class RaidMap : MonoBehaviour
{
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private UnitSpawnPoint[] _resourcesSpawnPoints;
    [SerializeField] private UnitSpawnPoint[] _objectsSpawnPoints;
    [SerializeField] private UnitSpawnPoint[] _enemiesSpawnPoints;

    private Unit[] _resources;
    private Unit[] _enemies;
    private Unit[] _objects;
    private Coroutine _spawningResources;
    private Coroutine _spawningObjects;
    private Coroutine _spawningEnemies;

    public StartPoint StartPoint => _startPoint;


    public void SpawnUnit(Unit[] resources, Unit[] enemies, Unit[] objects, Player player)
    {
        _resources = resources;
        _enemies = enemies;
        _objects = objects;

        _spawningResources = StartCoroutine(SpawningUnits(_resourcesSpawnPoints, _resources, player));
        _spawningObjects = StartCoroutine(SpawningUnits(_objectsSpawnPoints, _objects, player));
        _spawningEnemies = StartCoroutine(SpawningUnits(_enemiesSpawnPoints, _enemies, player));
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
