using UnityEngine;
using UnityEngine.Events;

public class TankAI : MonoBehaviour
{
    private Transform target; // Цель (башня)
    [SerializeField] private float speed = 3f; // Скорость танка

    public UnityAction DiedTank;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target == null) return;

        // Движение к цели
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Поворот в сторону цели
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Destroy(gameObject); // Удаляем танк при столкновении с башней
            DiedTank?.Invoke();
        }
    }
}
