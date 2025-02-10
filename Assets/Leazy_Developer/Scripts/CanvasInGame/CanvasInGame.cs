using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInGame : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;

    private void OnEnable()
    {
        MainWeaponHealth.OnLoseAction += OnLose;
        GameManager.OnWinAction += OnWin;
    }

    private void OnDisable()
    {
        MainWeaponHealth.OnLoseAction -= OnLose;
        GameManager.OnWinAction -= OnWin;
    }

    private void OnLose()
    {
        _losePanel.gameObject.SetActive(true);
    }

    private void OnWin()
    {
        _losePanel.gameObject.SetActive(true);
    }
}
