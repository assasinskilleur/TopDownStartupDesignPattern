public interface IHealth
{
    public bool IsPlayer();
    public void TakeDamage(float damage);
    public void Heal(float heal);
}
