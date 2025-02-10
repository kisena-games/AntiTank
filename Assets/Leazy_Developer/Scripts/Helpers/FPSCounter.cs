using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float _updateDelay = 0.2f;
    [SerializeField] private int _maxFPSDisplayed = 300;

    private TextMeshProUGUI _textMeshPro;
    private float _elapsedTime = 0.0f;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _maxFPSDisplayed = Mathf.Clamp(_maxFPSDisplayed, 0, GameManager.StringNumbers.Length);
    }

    private void Update()
    {
        _elapsedTime += Time.unscaledDeltaTime;

        if (_elapsedTime >= _updateDelay)
        {
            int currentFPS = (int)(1 / Time.unscaledDeltaTime);
            currentFPS = Mathf.Clamp(currentFPS, 0, _maxFPSDisplayed);

            _textMeshPro.text = GameManager.StringNumbers[currentFPS];
            _elapsedTime = 0.0f;
        }
    }
}
