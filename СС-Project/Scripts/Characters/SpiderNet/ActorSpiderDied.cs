using UnityEngine;

public class ActorSpiderDied : MonoBehaviour
{
    [SerializeField] private Character _character;
    [Space]
    [Header("ToDisable")] 
    [SerializeField] private SpawnNetTimer _spawnNetTimer;
    [SerializeField] private WayFollowSetter _wayFollowSetter;
    [SerializeField] private ActorRangeAttack _actorRangeAttack;

    private void OnEnable() => 
        _character.Died += OnCharacterDied;

    private void OnDisable() => 
        _character.Died -= OnCharacterDied;

    private void OnCharacterDied(Character character)
    {
        _spawnNetTimer.enabled = false;
        _wayFollowSetter.enabled = false;
        _actorRangeAttack.enabled = false;
    }
}
