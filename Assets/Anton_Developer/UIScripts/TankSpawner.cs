using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Radar radar;
    [SerializeField] private Transform tower; // Башня (цель для танков)
    [SerializeField] private float spawnInterval = 1f; // Интервал спавна

    private void Start()
    {
        InvokeRepeating(nameof(SpawnTank), 0f, spawnInterval); // Запускаем спавн каждые 1 секунду
    }

    private void SpawnTank()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject tank = Instantiate(tankPrefab, spawnPoint.position, Quaternion.identity);

        TankAI tankAI = tank.GetComponent<TankAI>(); // Берем скрипт TankAI у танка
        if (tankAI != null)
        {
            tankAI.SetTarget(tower); // Назначаем башню целью
        }

        radar.RegisterTank(tank.transform); // Добавляем танк на радар
    }
}
