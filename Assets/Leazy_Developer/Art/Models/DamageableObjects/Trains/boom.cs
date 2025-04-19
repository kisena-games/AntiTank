using UnityEngine;

public class SwitchObjects : MonoBehaviour
{
    public GameObject object1; // ������ ������
    public GameObject object2; // ������ ������
    public ParticleSystem particleSystem; // ������� ������

    void Start()
    {
        // ���������� ���������� ������ ������ ������ � ������� ������
        object1.SetActive(true);
        object2.SetActive(false);
        particleSystem.Stop(); // ������������� ������� ������
    }

    void Update()
    {
        // ���������, ��������� �� ������� ������ ������ ����
        if (Input.GetMouseButtonDown(1)) // 1 - ��� ������ ������ ����
        {
            ToggleObjects();
        }
    }

    void ToggleObjects()
    {
        object1.SetActive(false); // ������ ������ ������ ����������
        object2.SetActive(true); // ������ ������ ������ ��������
        particleSystem.Play(); // ��������� ������� ������
    }
}
