using UnityEngine;

public class PatrolAfterRestTransition : Transition
{
    [SerializeField] private float _delayTime = 2.0F;

    private float _elapsedTime = 0;

    private void Update()
    {
        if (_elapsedTime <= _delayTime)
        {
            _elapsedTime += Time.deltaTime;
            return;
        }
        NeedTransite = true;
        _elapsedTime = 0;
    }
}
