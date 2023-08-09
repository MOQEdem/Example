using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BowShooter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _startArrowTransform;
    [SerializeField] private Arrow _arrowTemplete;
    [SerializeField] private UnitType[] _unitTypes;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Unit _unit;

    private float _startDelay = 0.5f;
    private float _damage;
    private Coroutine _waitBeginShooting;
    private Vector3 _target;
    private Arrow _arrow;
    public event UnityAction SootEnded;
    public event UnityAction ArrowShooted;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Attack(Unit target)
    {
        _target = target.transform.position;
        if (_waitBeginShooting != null)
        {
            StopCoroutine(_waitBeginShooting);
        }
        _waitBeginShooting = StartCoroutine(WaitBeginShooting(_startDelay));
    }

    //Ñall from the Animator
    public void ShootArrow()
    {
        _arrow = Instantiate(_arrowTemplete, _startArrowTransform.position, _startArrowTransform.rotation);
        _arrow.SetTypesTargets(_unitTypes);
        _arrow.Shoot(_target, _weapon, _unit);
    }

    //Ñall from the Animator
    public void EndShoot()
    {
        SootEnded?.Invoke();
    }

    private IEnumerator WaitBeginShooting(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetTrigger(AnimatorConst.Shoot);
    }
}
