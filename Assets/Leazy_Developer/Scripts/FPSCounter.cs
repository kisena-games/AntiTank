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
    private string[] _stringNumbers;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _stringNumbers = new string[_maxFPSDisplayed + 1];

        for (int i = 0; i < _stringNumbers.Length; i++)
        {
            _stringNumbers[i] = i.ToString();
        }
    }

    private void Update()
    {
        _elapsedTime += Time.unscaledDeltaTime;

        if (_elapsedTime >= _updateDelay)
        {
            int currentFPS = (int)(1 / Time.unscaledDeltaTime);
            currentFPS = Mathf.Clamp(currentFPS, 0, _maxFPSDisplayed);

            _textMeshPro.text = _stringNumbers[currentFPS];
            _elapsedTime = 0.0f;
        }
    }
}
