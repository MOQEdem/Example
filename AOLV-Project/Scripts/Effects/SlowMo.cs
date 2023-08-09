using UnityEngine;

public class SlowMo : MonoBehaviour
{
    [SerializeField] private float _targetTimeScale;
    [SerializeField] private float _duration;

    private float _startTimeScale = 1;
    private float _startFixedDeltaTime;
    private float _currentTimeScale = 0;
    private void Awake()
    {
        _currentTimeScale = Time.timeScale;
        _startTimeScale = Time.timeScale;
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void OnDisable()
    {
        SetTimeScale(_startTimeScale);
    }
    public void Activate()
    {
        SetTimeScale(_targetTimeScale);
    }

    public void Activate(float targetTimeScale)
    {
        SetTimeScale(targetTimeScale);
    }

    public void Deactivate()
    {
        SetTimeScale(_startTimeScale);      
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
        _currentTimeScale = Time.timeScale;       
    }
}
