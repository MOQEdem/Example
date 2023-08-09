using System.Collections.Generic;
using UnityEngine;

public class DragonBossMeleeZone : MonoBehaviour
{
    private List<Character> _npcs = new List<Character>();

    public bool IsHaveTargets => _npcs.Count > 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log('f');

        if (other.TryGetComponent<Character>(out Character character))
        {
            Debug.Log('g');

            if (character.CharacterType == CharacterType.Player)
            {
                _npcs.Add(character);
                character.Died += OnNPCDied;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            if (character.CharacterType == CharacterType.Player)
            {
                _npcs.Remove(character);
                character.Died -= OnNPCDied;
            }
        }
    }

    private void OnNPCDied(Character npc)
    {
        _npcs.Remove(npc);
        npc.Died -= OnNPCDied;
    }
}
