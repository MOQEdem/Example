using UnityEngine;

public class RestState : State
{
    [SerializeField] private Animator _enemyAnimator;

    protected void OnEnable()
    {
        _enemyAnimator.SetFloat(AnimatorConst.Speed, 0f);
    }
}
