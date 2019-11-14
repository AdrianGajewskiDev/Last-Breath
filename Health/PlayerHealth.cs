using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public event System.Action OnHit;
    public void Die()
    {
        UIManager.Singleton.DeathScreenFadeIn();
        ZombiesManager.Singleton.DisableZombies();
        ResetHealth();
    }

    public void GiveDamage(int ammount)
    {
        currentHealth -= ammount;

        OnHit?.Invoke();
    }

    public bool IsDead() => currentHealth <= 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (IsDead())
            Die();
    }

    void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
