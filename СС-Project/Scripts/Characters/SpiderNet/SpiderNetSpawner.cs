using System.Linq;
using UnityEngine;

public class SpiderNetSpawner : MonoBehaviour
{
    [SerializeField] private SpiderNet _spiderNetPrefab;
    [SerializeField] [Min(0)] private float _spawnRadius = 2;

    private readonly Collider[] _collidersBuffer = new Collider[9];
    private Ground _groundPartToSpawn;

    [ContextMenu("SpawnNetNear")]
    public void ThrowNetNear()
    {
        Vector3? spawnPoint = GetRandomPosition();

        if (spawnPoint != null) 
            Spawn().Mover.MoveTo((Vector3) spawnPoint);
    }

    private SpiderNet Spawn() => 
        Instantiate(_spiderNetPrefab, transform.position, Quaternion.identity);

    public void ThrowNetTo(Vector3 position)
    {
        Vector3 point = new(Mathf.Round(position.x), 0, Mathf.Round(position.z));
        Spawn().Mover.MoveTo(point);
    }

    private Vector3? GetRandomPosition()
    {
        Vector3 point = Random.insideUnitCircle * _spawnRadius;
        point = new Vector3(Mathf.Round(point.x + transform.position.x), 0, Mathf.Round(point.y + transform.position.z));

        Physics.OverlapSphereNonAlloc(point, 0.5f, _collidersBuffer);

        if (_collidersBuffer != null && _collidersBuffer.Any(collider => collider?.GetComponent<SpiderNet>()))
            return null;

        return point;
    }
}
