using UnityEngine;

public class NetDestroyer : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private ParticleSystem _swordParticles;
    [SerializeField] [Min(0)] private float _destroyNetDelay = 0.4f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SpiderNet net))
        {
            _characterAnimator.Attack();
            _swordParticles.Play();
            Destroy(net.gameObject, _destroyNetDelay);
        }
    }
}
