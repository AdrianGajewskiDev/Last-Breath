using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public event System.Action OnHit;
    public void Die()
    {
        Debug.Log("We died :(");
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
}
