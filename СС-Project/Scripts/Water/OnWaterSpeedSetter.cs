using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWaterSpeedSetter : MonoBehaviour
{
    [SerializeField][Min(0)] private float _speedReductionFactor = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            if (!playerMover.IsMounted)
                playerMover.SetWaterSpeed(playerMover.Speed * _speedReductionFactor);
        }

        if (other.TryGetComponent(out PlayerCharacter npc))
        {
            if (npc.IsEliteNPC || npc.IsHeavy)
            {
                return;
            }

            if (npc.TryGetComponent(out CharacterMove characterMove))
            {
                characterMove.SetSpeedMove(characterMove.Speed * _speedReductionFactor);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            if (!playerMover.IsMounted)
                playerMover.SetDefaultSpeed();
        }

        if (other.TryGetComponent(out PlayerCharacter npc))
        {
            if (npc.IsEliteNPC || npc.IsHeavy)
            {
                return;
            }

            if (npc.TryGetComponent(out CharacterMove characterMove))
            {
                characterMove.SetSpeedMove(characterMove.Speed);
            }
        }
    }
}
