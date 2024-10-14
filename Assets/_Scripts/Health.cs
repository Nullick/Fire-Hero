using System;
using UnityEngine;

public class Health 
{
    public event Action<float> Changed;
    public event Action Died;

    [SerializeField] private float _value;

    public float Value => _value;

    public Health(float value)
    {
        _value = value;
    }

    public void Reduce(float value)
    {
        if(value < 0)
            throw new ArgumentException(nameof(value));


        if (_value <= 2f)
            Died?.Invoke();
        else
        {
            _value -= value;

            Changed?.Invoke(_value);
        }
    }
}
