using System;

public class TimerBrain
{
    private float _totalTime;
    private float _timeLeft;

    public float TimeLeft => _timeLeft;
    public float TotalTime => _totalTime;
    public float Progres => (_totalTime - _timeLeft) / _totalTime;

    public event Action Started;
    public event Action Stopped;
    public event Action Completed;
    public event Action Updated;

    public void Start(float time)
    {
        _totalTime = time;
        _timeLeft = time;

        Started?.Invoke();
    }

    public void Tick(float tick)
    {
        if (_timeLeft == 0)
            return;

        _timeLeft -= tick;
        Updated?.Invoke();

        if (_timeLeft <= 0)
        {
            _timeLeft = 0;
            Completed?.Invoke();
        }
    }

    public void Stop()
    {
        _timeLeft = 0;
        Stopped?.Invoke();
    }
}
