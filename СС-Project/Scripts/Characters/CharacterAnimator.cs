using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private RidingTransportAnimator _transportAnimator;

    private const string IsRun = "Run";
    private const string AttackTrigger = "AttackTrigger";
    private const string DieTrigger = "DieTrigger";
    private const string VictoryTrigger = "VictoryTrigger";
    private const string ResurrectionTrigger = "ResurrectionTrigger";
    private const string IsRide = "IsRide";
    private const string RangeAttack = "RangeAttack";

    public float CurrentAnimationTime => _animator.GetCurrentAnimatorStateInfo(0).length;
    public Animator Animator => _animator;

    public void Run(bool value)
    {
        _animator.SetBool(IsRun, value);

        if (_transportAnimator)
            _transportAnimator.SetRun(value);
    }

    public void Attack()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    public void Die()
    {
        _animator.SetTrigger(DieTrigger);
    }

    public void Victory()
    {
        _animator.SetTrigger(VictoryTrigger);

        if (_transportAnimator)
            _transportAnimator.SetVictory();
    }

    public void Resurrect()
    {
        _animator.SetTrigger(ResurrectionTrigger);
    }

    public void SetRangeAttack(bool isRange)
    {
        _animator.SetBool(RangeAttack, isRange);
    }

    public void SetCustomBool(string triggerName, bool value)
    {
        _animator.SetBool(triggerName, value);
    }

    public void SetCustomTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void SetRidingStatus(bool isRiding)
    {
        _animator.SetBool(IsRide, isRiding);
    }

    public void Add(RidingTransportAnimator transportAnimator)
    {
        _transportAnimator = transportAnimator;
        _animator.SetBool(IsRide, true);
    }
}