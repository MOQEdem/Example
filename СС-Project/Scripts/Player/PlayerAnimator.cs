using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string RUN = "Run";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.SetBool(RUN, true);
    }

    public void Idle()
    {
        _animator.SetBool(RUN, false);
    }
}
