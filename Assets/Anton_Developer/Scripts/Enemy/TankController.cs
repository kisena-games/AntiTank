using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private Transform _player; // Ссылка на игрока
    [SerializeField] private float _attackRange = 10f; // Расстояние для атаки
    [SerializeField] private float _moveSpeed = 2f; // Скорость движения
    [SerializeField] private Animator _trackAnimator; // Аниматор для гусениц
    [SerializeField] private Animator _turretAnimator; // Аниматор для башни
    [SerializeField] private string _moveAnimationParam = "IsMoving"; // Параметр для анимации гусениц
    [SerializeField] private string _attackAnimationParam = "IsAttacking"; // Параметр для анимации башни

    private bool _isAttacking = false;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer > _attackRange)
        {
            // Танк движется к игроку
            MoveTowardsPlayer();
            _isAttacking = false;
        }
        else
        {
            // Танк в зоне атаки
            StopMovement(); // Остановить движение танка
            AttackPlayer();
        }

        // Обновляем анимации
        UpdateAnimations();
    }

    private void StopMovement()
    {
        // Убедитесь, что танк не двигается, когда он в зоне атаки
        _isAttacking = true;
        _trackAnimator.SetBool("IsMoving", false);
    }

    private void MoveTowardsPlayer()
    {
        // Двигаемся к игроку
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.position += direction * _moveSpeed * Time.deltaTime;

        // Поворачиваем танк в сторону игрока
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _moveSpeed);
    }

    private void AttackPlayer()
    {
        _isAttacking = true;

        // Здесь можно добавить логику атаки (например, стрельбу из пушки)
    }

    private void UpdateAnimations()
    {
        if (_trackAnimator != null)
        {
            _trackAnimator.SetBool(_moveAnimationParam, !_isAttacking); // Анимация движения гусениц
        }

        if (_turretAnimator != null)
        {
            _turretAnimator.SetBool(_attackAnimationParam, _isAttacking); // Анимация атаки башни
        }
    }
}
