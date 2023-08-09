using UnityEngine;

public class ActorRangeAttack : MonoBehaviour
{
    [SerializeField] private TriggerObserver _rangeAttackTrigger;
    [SerializeField] private SpiderNetSpawner _netSpawner;
    [SerializeField] private CharacterType _detectedType;
    [SerializeField] [Min(0)] private int _throwCount = 3;

    private void OnEnable() => 
        _rangeAttackTrigger.TargetFound += OnTargetFound;

    private void OnDisable() => 
        _rangeAttackTrigger.TargetFound -= OnTargetFound;

    private void OnTargetFound(Character character)
    {
        if(character.CharacterType != _detectedType)
            return;
        
        if (_throwCount > 0)
        {
            _netSpawner.ThrowNetTo(character.transform.position);
            _throwCount--;
        }
    }
}
