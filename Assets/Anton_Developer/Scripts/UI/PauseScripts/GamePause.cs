using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static GamePause Instance;

    [SerializeField] GameObject _pausePanel;

    public bool IsPause { get; private set; }

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

        if (IsPause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        GameManager.Instance.SetPause(isEnable);
        _pausePanel.SetActive(IsPause);
    }
}
