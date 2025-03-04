using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInGame : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 2f;
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
        StartCoroutine(LoseWinWithDelay(_losePanel.gameObject));
    }

    private void OnWin()
    {
        StartCoroutine(LoseWinWithDelay(_winPanel.gameObject));
    }

    private void SetMenuCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private IEnumerator LoseWinWithDelay(GameObject window)
    {
        SetMenuCursor();

        float elapsedTime = 0f;
        Color color = _fadeImage.color;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / _fadeDuration);
            _fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        _fadeImage.color = color;

        window.SetActive(true);
    }
}
