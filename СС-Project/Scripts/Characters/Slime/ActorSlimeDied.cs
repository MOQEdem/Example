using UnityEngine;

public class ActorSlimeDied : MonoBehaviour
{
    [SerializeField] private EnemyCharacter _enemyCharacter;
    [SerializeField] private SlimePartsSpawner _slimePartsSpawner;

    private void OnEnable() => 
        _enemyCharacter.Died += OnDied;

    private void OnDisable() => 
        _enemyCharacter.Died -= OnDied;

    private void OnDied(Character character) => 
        _slimePartsSpawner.Spawn();
}
