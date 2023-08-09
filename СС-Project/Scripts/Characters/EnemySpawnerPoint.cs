using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnerPoint : MonoBehaviour
{
    [SerializeField] private NPC _prefab;
    [SerializeField] private float _timeToSpawn;

    private Coroutine _spawning;
    private Material _material;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    public void StartToSpawn(EnemySquad squad, Vector3 pointToMove)
    {
        if (_spawning == null)
        {
            _spawning = StartCoroutine(Spawning(squad, pointToMove));
        }
    }

    private IEnumerator Spawning(EnemySquad squad, Vector3 pointToMove)
    {
        float movingTime = 1.5f;

        Tween moving = transform.DOMove(pointToMove, movingTime);
        yield return moving.WaitForCompletion();

        var delay = new WaitForSeconds(_timeToSpawn);

        yield return delay;

        var npc = Instantiate(_prefab, this.transform.position, Quaternion.identity);
        npc.transform.parent = squad.transform;
        squad.AddUnit(npc);

        // float currentDissolveValue = 0;

        //while (_material.GetFloat("_DissolveAmount") < 1)
        //{
        //    currentDissolveValue += Time.deltaTime / 2;
        //    _material.SetFloat("_DissolveAmount", currentDissolveValue);
        //    yield return null;
        //}

        _meshRenderer.enabled = false;

        Destroy(this.gameObject);
    }
}
