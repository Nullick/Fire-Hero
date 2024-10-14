using System;

public class Complicator
{
    private Score _score;
    private Timer _timer;

    private float _complicatedTime = 60f;
    private float _startComplicatedTime;

    public event Action ComplicatedByScore;
    public event Action ComplicatedByTime;

    public Complicator(Score score, Timer timer)
    {
        _score = score;
        _score.Changed += OnScoreChanged;

        _timer = timer;
        _timer.Set(_complicatedTime);
        _timer.StartCountingTime();

        _startComplicatedTime = _complicatedTime;

        _timer.TimeIsOver += OnTimeOver;
    }

    private void OnScoreChanged()
    {
        if (_score.Value % 5 == 0 && _score.Value != 0)
            ComplicatedByScore?.Invoke();
    }

    private void OnTimeOver()
    {
        _complicatedTime += _startComplicatedTime * .5f;
        ComplicatedByTime?.Invoke();
    }

    ~Complicator()
    {
        _score.Changed -= OnScoreChanged;
        _timer.TimeIsOver -= OnTimeOver;
    }
}
