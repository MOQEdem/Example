using System;
using System.Collections;
using UnityEngine;

public class GameStartBossShower : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _animator;

    private const string _runAway = "RunAway";

    public event Action AnimationPlayed;

    public void StartPlayAnimation()
    {
        StartCoroutine(PlayingAnimation());
    }

    private IEnumerator PlayingAnimation()
    {
        _animator.SetCustomTrigger(_runAway);

        yield return null;
        yield return new WaitForSeconds(_animator.CurrentAnimationTime);

        transform.localScale = Vector3.zero;

        AnimationPlayed?.Invoke();
        Destroy(gameObject, 0.1f);
    }
}
