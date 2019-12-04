using LB.AI;
using LB.GameMechanics;
using UnityEngine;

namespace LB.Health
{
    public class ZombieHealth : MonoBehaviour, IHealth
    {
        public float MaxHealth;

        public float currentHealth;

        Animator animator;

        public event System.Action OnHit;
        public event System.Action OnDie;

        private void Awake()
        {
            currentHealth = MaxHealth;
            animator = GetComponent<Animator>();
        }

        public bool IsDead() => currentHealth <= 0;

        public void Die()
        {
            if (!IsDead())
                return;

            animator.SetBool("Died", true);
            GetComponent<ZombieAI>().enabled = false;
            GetComponent<AudioSource>().Stop();
            Destroy(gameObject, 5);


        }

        private void OnDisable()
        {
            if (!ZombiesManager.Singleton.gameOver)
                OnDie?.Invoke();
        }

        private void Update()
        {
            Die();
        }

        public void GiveDamage(int ammount)
        {
            currentHealth -= ammount;
            OnHit?.Invoke();
        }
    }

}
