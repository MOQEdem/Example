using UnityEngine;

public class RidingTransportAnimator : MonoBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");
    private const string Victory = "VictoryTrigger";

    [SerializeField] private Animator _animator;

    public void SetRun(bool active) =>
        _animator.SetBool(Move, active);

    public void SetVictory() =>
    _animator.SetTrigger(Victory);
}