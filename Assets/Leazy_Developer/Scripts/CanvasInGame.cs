using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInGame : MonoBehaviour
{
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;

    private void OnEnable()
    {
        MainWeaponHealth.OnLoseAction += OnLose;
    }

    private void OnDisable()
    {
        MainWeaponHealth.OnLoseAction -= OnLose;
    }

    private void OnLose()
    {
        _inGameUI.SetActive(false);
        _losePanel.gameObject.SetActive(true);
    }
}
