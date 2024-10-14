using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Brick> _bricks = new List<Brick>();

    //private Score _score;

    public static event Action EnemyDied;
    public static EnemyManager Instance;

    public void Initialize()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;

        MoveBricks();
        CheckBricks();
    }

    private void MoveBricks()
    {
        foreach (var brick in _bricks)
        {
            brick.Move();
        }
    }

    private void CheckBricks()
    {
        for (int i = 0; i < _bricks.Count; i++)
        {
            if (_bricks[i].Bnd.OffDown)
            {
                Destroy(_bricks[i].gameObject);
                _bricks.RemoveAt(i);
            }
        }
    }

    public void OnEnemyDied()
    {
        EnemyDied?.Invoke();
    }

    public void AddList(Brick brick)
    {
        _bricks.Add(brick);
    }

    public void RemoveList(Brick brick)
    {
        _bricks.Remove(brick);
    }
}