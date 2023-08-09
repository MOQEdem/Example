using System.Collections.Generic;
using UnityEngine;

public class SpeedReductionZone : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private CharacterType _detectedType;
    [SerializeField][Min(0)] private float _speedReductionFactor = 0.5f;

    private List<CharacterMove> _characterMoves = new List<CharacterMove>();

    private void Start()
    {
        _triggerObserver.TargetFound += OnTargetFound;
        _triggerObserver.TargetLost += OnTargetLost;
    }

    private void FixedUpdate()
    {
        if (_characterMoves.Count == 0)
        {
            enabled = false;
            return;
        }

        foreach (CharacterMove move in _characterMoves)
            if (move != null)
                move.SetSpeedMove(move.Speed * _speedReductionFactor);
    }

    private void OnDestroy()
    {
        foreach (CharacterMove move in _characterMoves)
            if (move != null)
                move.SetSpeedMove(move.Speed);
    }

    private void OnTargetFound(Character character)
    {
        if (character.CharacterType != _detectedType)
            return;

        if (character.TryGetComponent(out CharacterMove move))
        {
            if (move != null)
            {

                move.SetSpeedMove(move.Speed * _speedReductionFactor);
                _characterMoves.Add(move);
                character.Died += OnCharacterDied;
                enabled = true;
            }
        }
    }

    private void OnCharacterDied(Character character)
    {
        character.Died -= OnCharacterDied;
        if (character.TryGetComponent(out CharacterMove move))
            _characterMoves.Remove(move);
    }

    private void OnTargetLost(Character character)
    {
        if (character.CharacterType != _detectedType)
            return;

        if (character.TryGetComponent(out CharacterMove move))
        {
            if (move != null)
                move.SetSpeedMove(move.Speed);

            _characterMoves.Remove(move);
        }
    }
}
