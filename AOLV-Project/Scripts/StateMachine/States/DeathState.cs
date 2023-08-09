using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))] 

public class DeathState : State
{
    [SerializeField] private float _durationStun = 0.1f;

    private float _durationAnimation;
    private Animator _animator;
    private Enemy _enemy;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        _durationAnimation = stateInfo.length;
        _enemy = GetComponent<Enemy>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {        
        StartCoroutine(CountDownTimeToDeath(_durationStun,_durationAnimation));
    }

    private IEnumerator CountDownTimeToDeath(float durationStun, float durationAnimation)
    {     
        yield return new WaitForSeconds(durationStun);
        _rigidbody.velocity = Vector2.zero;        
    }
}
