using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private PlayableCharacter _player;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private CatapultFireSystem _catapult;
    [SerializeField] private DragonLair _dragonLair;
    // [SerializeField] private GoldADSReward _reward;
    [SerializeField] private Stable _stable;

    private AnalyticManager _analytic;

    private void Awake()
    {
        _analytic = GetComponent<AnalyticManager>();
    }

    private void Start()
    {
        _analytic.SendEventOnLevelStart();
    }

    private void OnEnable()
    {
        _player.Resurrected += OnPlayerRevie;
        _player.Died += OnPlayerDied;
        _loader.LevelFinised += OnLevelComplite;
        _loader.LevelRestarted += OnLevelRestarted;
        _catapult.TNTLaunched += OnCatapultUsed;
        _dragonLair.DragonLaunched += OnDragonUsed;
        //  _reward.RewardUsed += OnGoldRewardUsed;
        _stable.MountUsed += OnMountUsed;
    }

    private void OnDisable()
    {
        _player.Resurrected -= OnPlayerRevie;
        _player.Died -= OnPlayerDied;
        _loader.LevelFinised -= OnLevelComplite;
        _loader.LevelRestarted -= OnLevelRestarted;
        _catapult.TNTLaunched -= OnCatapultUsed;
        _dragonLair.DragonLaunched -= OnDragonUsed;
        //   _reward.RewardUsed -= OnGoldRewardUsed;
        _stable.MountUsed -= OnMountUsed;
    }

    private void OnLevelComplite() => _analytic.SendEventOnLevelComplete();

    private void OnPlayerDied(Character character) => _analytic.SendEventOnFail();

    private void OnLevelRestarted() => _analytic.SendEventOnLevelRestart();

    private void OnPlayerRevie() => _analytic.SendEventOnReviveUsed();

    private void OnCatapultUsed(TNT tnt) => _analytic.SendEventOnCatapultUsed();

    private void OnDragonUsed(DragonPointer pointer) => _analytic.SendEventOnDragonUsed();

    private void OnGoldRewardUsed() => _analytic.SendEventOnGoldRewardUsed();

    private void OnMountUsed() => _analytic.SendEventOnMountUsed();
}
