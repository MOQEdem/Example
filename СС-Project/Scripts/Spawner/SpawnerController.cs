using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private PlayerSquad _playerSquad;
    [SerializeField] private CharacterProduser[] _unitSpawners;

    private void Awake()
    {
        _unitSpawners = GetComponentsInChildren<CharacterProduser>(true);
    }

    private void OnEnable()
    {
        _playerSquad.BattleStarted += OnBattleStarted;
        _playerSquad.SquadEmpty += OnPlayerSquadEmpty;
    }

    private void OnDisable()
    {
        _playerSquad.BattleStarted -= OnBattleStarted;
        _playerSquad.SquadEmpty -= OnPlayerSquadEmpty;
    }

    private void OnBattleStarted()
    {
        foreach (var unitSpawner in _unitSpawners)
            unitSpawner.DisableSpawning();
    }

    private void OnPlayerSquadEmpty(Squad squad)
    {
        foreach (var unitSpawner in _unitSpawners)
            unitSpawner.EnableSpawning();
    }
}
