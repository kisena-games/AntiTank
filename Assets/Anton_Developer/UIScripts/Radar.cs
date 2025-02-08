using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform tower; // Центр радара (башня)
    [SerializeField] private RectTransform radarUI; // UI-элемент радара
    [SerializeField] private GameObject tankIconPrefab; // Префаб иконки танка
    [SerializeField] private float radarSize = 115f; // Радиус радара

    private List<GameObject> tankIcons = new List<GameObject>(); // Иконки на радаре
    private List<Transform> enemies = new List<Transform>(); // Список врагов

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (i >= tankIcons.Count) continue;

            Vector3 enemyPos = enemies[i].position;
            Vector3 relativePos = enemyPos - tower.position;
            Vector2 radarPos = new Vector2(relativePos.x, relativePos.z) / radarSize;
            radarPos = Vector2.ClampMagnitude(radarPos, 0.5f) * radarUI.rect.width;

            tankIcons[i].GetComponent<RectTransform>().anchoredPosition = radarPos;
        }
    }

    // Метод для регистрации танка на радаре
    public void RegisterTank(Transform enemy)
    {
        GameObject icon = Instantiate(tankIconPrefab, radarUI);
        tankIcons.Add(icon);
        enemies.Add(enemy);
    }
}
