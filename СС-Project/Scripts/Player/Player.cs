using UnityEngine;

public class Player : MonoBehaviour
{
    public int CurrentLevel { get; private set; } = 1;
    public PlayableCharacter PlayableCharacter { get; private set; }
    public PlayerStack PlayerStack { get; private set; }
    public PlayerMover PlayerMover { get; private set; }
    public PlayerModel PlayerModel { get; private set; }
    public PlayerStatsManager PlayerStatsManager { get; private set; }
    public PlayerAccessToFreeADS PlayerAccess { get; private set; }
    public ResourceCollectorSwitcher ResourceCollectorSwitcher { get; private set; }
    public FloatingJoystick Joystick => PlayerMover.Joystick;

    private void Awake()
    {
        PlayerStack = GetComponent<PlayerStack>();
        PlayableCharacter = GetComponent<PlayableCharacter>();
        PlayerMover = GetComponent<PlayerMover>();
        PlayerModel = GetComponentInChildren<PlayerModel>();
        PlayerStatsManager = GetComponent<PlayerStatsManager>();
        PlayerAccess = GetComponent<PlayerAccessToFreeADS>();
        ResourceCollectorSwitcher = GetComponent<ResourceCollectorSwitcher>();
    }
}
