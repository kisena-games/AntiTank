using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirStrikeUI : MonoBehaviour
{
    [SerializeField] private Image _airStrikeImage;

    private float _blinkDuration = 0.5f;
    private float _scaleMultiplier = 1.2f;

    private void OnEnable()
    {
        StartCoroutine(StartBlinkUI());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartBlinkUI()
    {
        Color originalColor = _airStrikeImage.color;
        Color lighterColor = originalColor + new Color(0.2f, 0.2f, 0.2f, 0f);

        Vector3 originalScale = _airStrikeImage.rectTransform.localScale;
        Vector3 targetScale = originalScale * _scaleMultiplier;

        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < _blinkDuration)
            {
                float t = elapsedTime / _blinkDuration;
                _airStrikeImage.rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / _blinkDuration);
                _airStrikeImage.color = Color.Lerp(originalColor, lighterColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;

            while (elapsedTime < _blinkDuration)
            {
                float t = elapsedTime / _blinkDuration;
                _airStrikeImage.rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / _blinkDuration);
                _airStrikeImage.color = Color.Lerp(lighterColor, originalColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
