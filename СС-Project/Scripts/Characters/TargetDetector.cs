using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private CharacterType _detectedType;

    private List<Character> _targets = new List<Character>();

    public List<Character> Targets => _targets;

    public event Action TargetFound;

    private void OnDisable()
    {
        foreach (Character target in _targets)
            target.Died -= RemoveCharacter;

        _targets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            if (character.CharacterType == _detectedType)
            {
                _targets.Add(character);
                character.Died += RemoveCharacter;

                if (_targets.Count == 1)
                {
                    TargetFound?.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            if (character.CharacterType == _detectedType)
            {
                RemoveCharacter(character);
            }
        }
    }

    private void RemoveCharacter(Character character)
    {
        character.Died -= RemoveCharacter;
        _targets.Remove(character);
    }
}
