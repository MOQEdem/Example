using UnityEngine;

public class EnemySpawner : EnemyNPC
{
    [Space]
    [Header("SpawnerSettings")]
    [SerializeField] private EnemySpawnerPoint _prefab;
    [SerializeField] private Transform _pointsCenter;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private int _maxSquadUnitCount = 6;

    private EnemySquad _squad;
    private float _currentTime;

    protected override void Awake()
    {
        base.Awake();
        _squad = GetComponentInParent<EnemySquad>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!IsDead)
        {
            if (_squad.Units.Count <= _maxSquadUnitCount && Target == null)
            {
                _currentTime += Time.fixedDeltaTime;

                if (_currentTime >= _timeBetweenSpawn)
                {
                    Vector3 spawntPoint = _pointsCenter.position + Random.insideUnitSphere * 3;
                    spawntPoint.y = 0;

                    var egg = Instantiate(_prefab, this.transform.position, Quaternion.identity);
                    egg.StartToSpawn(_squad, spawntPoint);
                    _currentTime = 0;
                }
            }
            else
            {
                _currentTime = 0;
            }
        }
    }
}
