using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;

    private int _currentFPS = -1;

    private List<int> _keysFPS;
    private Dictionary<int, string> _keyValueFPS = new Dictionary<int, string>
    {
        { 20, "20" },
        { 30, "30" },
        { 40, "40" },
        { 60, "60" },
        { -1, "No limit" },
    };

    private void Awake()
    {
        _keysFPS = new List<int>(_keyValueFPS.Keys);
    }

    private void Start()
    {
        _currentFPS = Application.targetFrameRate;

        UpdateFPS();
    }

    public void LeftArrow()
    {
        ChangeFPS(-1);
    }

    public void RightArrow()
    {
        ChangeFPS(1);
    }

    private void ChangeFPS(int direction)
    {
        int index = _keysFPS.IndexOf(_currentFPS);

        index = Mathf.Clamp(index + direction, 0, _keysFPS.Count - 1);
        _currentFPS = _keysFPS[index];

        UpdateFPS();
    }

    private void UpdateFPS()
    {
        if (_currentFPS == -1)
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = -1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _currentFPS;
        }

        _fpsText.text = _keyValueFPS[_currentFPS];
    }
}
