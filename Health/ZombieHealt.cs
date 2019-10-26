using UnityEngine;

public class ZombieHealth : MonoBehaviour, IHealth
{
    public int MaxHealth;

    int currentHealth;

    Animator animator;

    public event System.Action<Animator> OnHit;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
    }
    public bool IsDead() => currentHealth > 0;

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void GiveDamage(int ammount)
    {
        currentHealth -= ammount;

        OnHit?.Invoke(animator);
    }
}
