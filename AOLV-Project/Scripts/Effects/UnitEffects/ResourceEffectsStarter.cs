using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class ResourceEffectsStarter : UnitEffectsStarter
{
    [SerializeField] private GameObject[] _destroyedChunks;

    private Collider _collider;
    private Tween _damageAnimationTween;
    private Vector3 _startScale;
    private NavMeshObstacle _meshObstacle;
    //  private bool _isDestroyed = false;

    private void Awake()
    {
        _startScale = transform.localScale;
        _collider = GetComponent<Collider>();
        _meshObstacle = GetComponent<NavMeshObstacle>();
    }

    protected override void OnUnitAttacked(AttackData attackData)
    {
        PlayDamageParticles();
        PlayDamageAnimation();
        TryDeactivateChunks();
        //ShowPopUpText(attackData.Weapon.Damage);
    }

    private void TryDeactivateChunks()
    {
        float remainingHealth = 1 - Unit.Health.HealthFill;
        int deactivationChunkCount = (int)(remainingHealth * _destroyedChunks.Length);
        for (int i = 0; i < deactivationChunkCount; i++)
        {
            _destroyedChunks[i].SetActive(false);
        }
    }

    private void PlayDamageAnimation()
    {
        var sequence = DOTween.Sequence();

        if (_damageAnimationTween != null && _damageAnimationTween.active == true)
            _damageAnimationTween.Complete();
        _damageAnimationTween = transform.DOScale(_startScale.x * 1.2f, 0.1f).SetLoops(2, LoopType.Yoyo);

        // transform.DORotate(new Vector3(0, 0, 30), 1f);
    }

    protected override void OnUnitDied(Unit unit)
    {
        //_isDestroyed = true;
        _collider.enabled = false;

        if (_meshObstacle != null)
            _meshObstacle.enabled = false;
    }
}
