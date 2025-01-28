public interface IDamageable
{
    public bool IsKilled { get; }
    public void TakeDamage(int damage);
}
