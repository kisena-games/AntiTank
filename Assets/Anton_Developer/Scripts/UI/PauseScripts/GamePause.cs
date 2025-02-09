using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static GamePause Instance;

    [SerializeField] GameObject _pausePanel;

    public bool IsPause { get; private set; }
    

    private List<IPauseHandler> _pauses = new List<IPauseHandler>();

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
        if (Input.GetKeyDown(KeyCode.RightAlt))
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

        _pausePanel.SetActive(IsPause);
    }

    public void AddPauseList(IPauseHandler pauseHandler)
    {
        if (!_pauses.Contains(pauseHandler))
        {
            _pauses.Add(pauseHandler);
        }
    }

    public void RemovePauseList(IPauseHandler pauseHandler)
    {
        _pauses.Remove(pauseHandler);
    }
}
