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
        _losePanel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnWin()
    {
        _winPanel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
