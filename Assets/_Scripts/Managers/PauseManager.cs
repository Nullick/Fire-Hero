using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour, IPauseHandler
{
    private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
    public static PauseManager Instance;
    public bool IsPaused { get; private set; }

    public void Initialize()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else        
            Instance = this;
    }

    private void OnDestroy()
    {
        if(Instance == this)
            Instance = null;
    }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }

    public void Unregister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        foreach(var handler in _handlers)
        {
            handler.SetPaused(isPaused);
        }
    }
}
