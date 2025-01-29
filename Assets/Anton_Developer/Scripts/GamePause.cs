using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public bool IsPause { get; private set; }

    public static GamePause Instance;

    private List<IsPauseHandler> _pauses = new List<IsPauseHandler>();

    private void Awake()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!IsPause);
        }
    }

    public void SetPause(bool isEnable)
    {
        IsPause = isEnable;

        for (int i = 0; i < _pauses.Count; i++)
        {
            _pauses[i].IsPaused(isEnable);
        }
    }

    public void AddPauseList(IsPauseHandler pauseHandler)
    {
        if (!_pauses.Contains(pauseHandler))
        {
            _pauses.Add(pauseHandler);
        }
    }

    public void RemovePauseList(IsPauseHandler pauseHandler)
    {
        _pauses.Remove(pauseHandler);
    }
}

public interface IsPauseHandler
{
    void IsPaused(bool isPaused);
}
