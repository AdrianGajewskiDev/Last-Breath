using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] int maxHealth;
    public int currentHealth;

    public void Die()
    {
        Debug.Log("We died :(");
    }

    public void GiveDamage(int ammount)
    {
        currentHealth -= ammount;
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
