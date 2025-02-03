using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform tower; // ����� ������ (�����)
    [SerializeField] private RectTransform radarUI; // UI-������� ������
    [SerializeField] private GameObject tankIconPrefab; // ������ ������ �����
    [SerializeField] private float radarSize = 115f; // ������ ������

    private List<GameObject> tankIcons = new List<GameObject>(); // ������ �� ������
    private List<Transform> enemies = new List<Transform>(); // ������ ������

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

    // ����� ��� ����������� ����� �� ������
    public void RegisterTank(Transform enemy)
    {
        GameObject icon = Instantiate(tankIconPrefab, radarUI);
        tankIcons.Add(icon);
        enemies.Add(enemy);
    }
}
