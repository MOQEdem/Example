using UnityEngine;

public class ActorSplashDamage : MonoBehaviour
{
    [SerializeField] private Arrow _arrow;
    [SerializeField] private SplashDamage _splashDamage;

    private void OnEnable() => 
        _arrow.TargetHit += OnTargetHit;

    private void OnDisable() => 
        _arrow.TargetHit -= OnTargetHit;

    private void OnTargetHit()
    {
        _splashDamage.MakeDamage();
    }
}
