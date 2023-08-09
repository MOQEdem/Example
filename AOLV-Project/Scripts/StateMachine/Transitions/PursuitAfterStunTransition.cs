using System.Collections;
using UnityEngine;

public class PursuitAfterStunTransition : Transition
{
    [SerializeField] private float _stunDuratin;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(WAitEndingDelay(_stunDuratin));
    }

    private IEnumerator WAitEndingDelay( float time)
    {
        yield return new WaitForSeconds(time);
        NeedTransite = true;
    }
}
