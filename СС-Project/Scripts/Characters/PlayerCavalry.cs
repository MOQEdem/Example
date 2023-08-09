using UnityEngine;

public class PlayerCavalry : PlayerCharacter
{
    [SerializeField]private RidingTransport _ridingTransport;
    
    private ActorTakeTransport _actorTakeTransport;

    protected override void Awake()
    {
        base.Awake();
        _actorTakeTransport = GetComponent<ActorTakeTransport>();
    }

    protected override void Start()
    {
        base.Start();
        _actorTakeTransport.Take(_ridingTransport);
    }
}
