using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasInGame : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private TextMeshProUGUI _currentTanksCount;
    [SerializeField] private TextMeshProUGUI _maxTanksCount;

    private int _tanksCount = 0;

    private void OnEnable()
    {
        GameManager.OnChangeTankCountAction += OnChangeTankCount;
        MainWeaponHealth.OnLoseAction += OnLose;
        GameManager.OnWinAction += OnWin;
    }

    private void OnDisable()
    {
        GameManager.OnChangeTankCountAction -= OnChangeTankCount;
        MainWeaponHealth.OnLoseAction -= OnLose;
        GameManager.OnWinAction -= OnWin;
    }

    private void OnChangeTankCount()
    {
        _currentTanksCount.text = GameManager.StringNumbers[++_tanksCount];
    }

    private void OnLose()
    {
        SetMenuCursor();
        _losePanel.gameObject.SetActive(true);
    }

    private void OnWin()
    {
        SetMenuCursor();
        _winPanel.gameObject.SetActive(true);
    }

    private void SetMenuCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
