
namespace LB.Health
{
    public interface IHealth
    {
        void GiveDamage(int ammount);
        void Die();

        bool IsDead();
    }

}
