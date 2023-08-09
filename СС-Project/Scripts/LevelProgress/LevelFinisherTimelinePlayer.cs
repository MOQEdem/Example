using UnityEngine;
using UnityEngine.Playables;

public class LevelFinisherTimelinePlayer : MonoBehaviour
{
    private LevelFinisher _finisher;

    private PlayableDirector _player;

    private void Awake()
    {
        _finisher = GetComponentInParent<LevelFinisher>();
        _player = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        _finisher.LevelComplite += OnReached;
    }

    private void OnDisable()
    {
        _finisher.LevelComplite += OnReached;
    }

    private void OnReached(LevelFinisher finisher)
    {
        _player.Play();
    }
}
