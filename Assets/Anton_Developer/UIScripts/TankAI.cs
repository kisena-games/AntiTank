using UnityEngine;
using UnityEngine.Events;

public class TankAI : MonoBehaviour
{
    private Transform target; // ���� (�����)
    [SerializeField] private float speed = 3f; // �������� �����

    public UnityAction DiedTank;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target == null) return;

        // �������� � ����
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // ������� � ������� ����
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Destroy(gameObject); // ������� ���� ��� ������������ � ������
            DiedTank?.Invoke();
        }
    }
}
