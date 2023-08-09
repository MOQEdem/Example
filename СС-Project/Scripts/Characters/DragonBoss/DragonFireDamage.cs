using UnityEngine;

public class DragonFireDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explodeParticles;
    [SerializeField] private LayerMask _targetsLayer;
    [SerializeField] private CharacterType _detectedType;
    [SerializeField] [Min(1)] private int _maxTargetsCount = 10;
    [SerializeField] [Min(0)] private float _damageRadius = 5;
    [SerializeField] [Min(0)] private int _damage = 10;

    private Collider[] _collidersBuffer;

    private void Start() => 
        _collidersBuffer = new Collider[_maxTargetsCount];

    public void Explode()
    {
        Instantiate(_explodeParticles, transform.position, Quaternion.identity).Play();
        Physics.OverlapSphereNonAlloc(transform.position, _damageRadius, _collidersBuffer, _targetsLayer);

        foreach (Collider other in _collidersBuffer)
            if (other.TryGetComponent(out Character character) && character.CharacterType == _detectedType)
                character.ApplyDamage(_damage);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0, 0.2f);
        Gizmos.DrawSphere(transform.position, _damageRadius);
    }
#endif
}
