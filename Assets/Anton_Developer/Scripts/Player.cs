using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _shootPoint;

    private int _currentHealth;

    public int Money { get; private set; }

    private void Start()
    {
        _currentHealth = _health;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnemyDied(int reward)
    {
        Money += reward;
    }
}
