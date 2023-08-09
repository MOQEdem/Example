using UnityEngine;

public class MeteorAttackTimer : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] [Min(0)] private float _maxCommonAttackInterval = 3f;
    [SerializeField] [Min(0)] private float _maxMeteorAttackInterval = 1f;
    [SerializeField] private AnimationCurve _randomCurve;

    private float _timer;
    private bool _isMeteorAttackEnable;

    private void Start() => 
        _timer = _maxCommonAttackInterval;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _isMeteorAttackEnable = !_isMeteorAttackEnable;
            _characterAnimator.SetRangeAttack(_isMeteorAttackEnable);
            _timer = _randomCurve.Evaluate(Random.Range(0, 1f));
            if (_isMeteorAttackEnable)
                _timer *= _maxMeteorAttackInterval;
            else
                _timer *= _maxCommonAttackInterval;
        }
    }
}
