using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private float _updateDelay = 1f;

    private TextMeshProUGUI _textMeshPro;
    private float _elapsedTime = 0.0f;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _elapsedTime += Time.unscaledDeltaTime;

        if (_elapsedTime >= _updateDelay)
        {
            int currentTime = (int)Time.timeSinceLevelLoad;

            _textMeshPro.text = currentTime.ToString();
            _elapsedTime = 0.0f;
        }
    }
}
