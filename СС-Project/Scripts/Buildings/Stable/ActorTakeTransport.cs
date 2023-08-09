using UnityEngine;

public class ActorTakeTransport : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;
    [SerializeField] private CharacterAnimator _playerAnimator;

    public void Take(RidingTransport transport)
    {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        transport.transform.SetParent(transform);
        _playerModel.transform.position = transport.Saddle.position;
        _playerAnimator.Add(transport.Animator);

        PlayerMover mover = GetComponent<PlayerMover>();
        mover?.SetNewSpeed(transport.Speed);
        mover?.SetMountedStatus();


        PlayableCharacter playableCharacter = GetComponent<PlayableCharacter>();
        playableCharacter?.UpgradeHealth(playableCharacter.MaxHealthValue + transport.HealthBonus);
    }
}
