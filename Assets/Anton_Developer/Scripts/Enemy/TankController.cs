using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private Transform _player; // ������ �� ������
    [SerializeField] private float _attackRange = 10f; // ���������� ��� �����
    [SerializeField] private float _moveSpeed = 2f; // �������� ��������
    [SerializeField] private Animator _trackAnimator; // �������� ��� �������
    [SerializeField] private Animator _turretAnimator; // �������� ��� �����
    [SerializeField] private string _moveAnimationParam = "IsMoving"; // �������� ��� �������� �������
    [SerializeField] private string _attackAnimationParam = "IsAttacking"; // �������� ��� �������� �����

    private bool _isAttacking = false;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer > _attackRange)
        {
            // ���� �������� � ������
            MoveTowardsPlayer();
            _isAttacking = false;
        }
        else
        {
            // ���� � ���� �����
            StopMovement(); // ���������� �������� �����
            AttackPlayer();
        }

        // ��������� ��������
        UpdateAnimations();
    }

    private void StopMovement()
    {
        // ���������, ��� ���� �� ���������, ����� �� � ���� �����
        _isAttacking = true;
        _trackAnimator.SetBool("IsMoving", false);
    }

    private void MoveTowardsPlayer()
    {
        // ��������� � ������
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += direction * _moveSpeed * Time.deltaTime;

        // ������������ ���� � ������� ������
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _moveSpeed);
    }

    private void AttackPlayer()
    {
        _isAttacking = true;

        // ����� ����� �������� ������ ����� (��������, �������� �� �����)
    }

    private void UpdateAnimations()
    {
        if (_trackAnimator != null)
        {
            _trackAnimator.SetBool(_moveAnimationParam, !_isAttacking); // �������� �������� �������
        }

        if (_turretAnimator != null)
        {
            _turretAnimator.SetBool(_attackAnimationParam, _isAttacking); // �������� ����� �����
        }
    }
}
