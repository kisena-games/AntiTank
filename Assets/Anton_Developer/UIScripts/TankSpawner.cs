using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Radar radar;
    [SerializeField] private Transform tower; // ����� (���� ��� ������)
    [SerializeField] private float spawnInterval = 1f; // �������� ������

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTank), 0f, spawnInterval); // ��������� ����� ������ 1 �������
    }

    private void SpawnTank()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject tank = Instantiate(tankPrefab, spawnPoint.position, Quaternion.identity);

        TankAI tankAI = tank.GetComponent<TankAI>(); // ����� ������ TankAI � �����
        if (tankAI != null)
        {
            tankAI.SetTarget(tower); // ��������� ����� �����
        }

        radar.RegisterTank(tank.transform); // ��������� ���� �� �����
    }
}
