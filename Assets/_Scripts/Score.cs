using System;

public class Score
{
    private int _value;

    public event Action<int> ChangedValue;
    public event Action Changed;

    public int Value => _value;

    public Score()
    {
        _value = 0;
    }

    public void Change()
    {
        _value++;

        ChangedValue?.Invoke(_value);
        Changed?.Invoke();
    }
}