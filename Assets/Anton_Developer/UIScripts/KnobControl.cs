using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class KnobControl : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public RectTransform knob;  // Ссылка на объект крутилки
    public TextMeshProUGUI valueText; // Текст для отображения процента
    public float minAngle = 0f;
    public float maxAngle = 270f;

    private float currentAngle;

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateKnob(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateKnob(eventData);
    }

    private void UpdateKnob(PointerEventData eventData)
    {
        Vector2 dir = eventData.position - (Vector2)knob.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        // Ограничение угла поворота
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        knob.rotation = Quaternion.Euler(0, 0, angle);

        // Перевод угла в проценты
        float percent = (angle - minAngle) / (maxAngle - minAngle) * 100;
        valueText.text = Mathf.RoundToInt(percent) + "%";
    }
}
