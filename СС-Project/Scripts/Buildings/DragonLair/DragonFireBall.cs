using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DragonFireBall : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;

    private TargetDetector _detector;
    private Coroutine _flying;
    private AudioSource _explosionSound;

    private void Awake()
    {
        _detector = GetComponentInChildren<TargetDetector>();
        _explosionSound = GetComponentInChildren<AudioSource>();
    }

    public void FlyToEnemy(Vector3 targetPositin, int damage)
    {
        if (_flying == null)
            _flying = StartCoroutine(Flying(targetPositin, damage));
    }

    private IEnumerator Flying(Vector3 targetPositin, int damage)
    {
        transform.parent = null;

        float _flyingTime = 1.5f;

        Tween flying = transform.DOMove(targetPositin, _flyingTime);
        yield return flying.WaitForCompletion();

        Character[] targets = _detector.Targets.ToArray();

        _explosionParticle.transform.parent = null;
        _explosionParticle.transform.rotation = Quaternion.Euler(Vector3.zero);
        _explosionParticle.Play();
        _explosionSound.Play();

        foreach (var target in targets)
        {
            target.ApplyDamage(damage);

            if (target is NPCWalker walker)
                walker.DoHitJump();
        }

        Destroy(this.gameObject, 0.1f);
        _flying = null;
    }
}
