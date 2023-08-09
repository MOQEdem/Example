using UnityEngine;

public class ActorSpiderSceletonDie : MonoBehaviour
{
    [SerializeField] private NPC _npc;
    [SerializeField] private Dismounter _dismounter;

    private void OnEnable() => 
        _npc.Died += OnNPCDied;

    private void OnDisable() => 
        _npc.Died -= OnNPCDied;

    private void OnNPCDied(Character npc) => 
        _dismounter.MakeDismount();
}
