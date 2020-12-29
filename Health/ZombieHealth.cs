using LB.AI;
using LB.GameMechanics;
using UnityEngine;

namespace LB.Health
{
    public class ZombieHealth : MonoBehaviour, IHealth
    {
        public float MaxHealth;

        public float currentHealth;
        public bool KilledByKnife;

        Animator animator;

        ZombieAI zombieAI;
        public event System.Action<ZombieAI> OnHit;
        public event System.Action OnDie;

        private void Awake()
        {
            currentHealth = MaxHealth;
            animator = GetComponent<Animator>();
            zombieAI = GetComponent<ZombieAI>();
        }

        public bool IsDead() => currentHealth <= 0;

        public void Die()
        {
            if (!IsDead())
                return;

            zombieAI.SetUpNavMeshAgent(false);
            zombieAI.enabled = false;

            if (KilledByKnife == true)
            {
                animator.SetBool("dieFromKnife", true);

            }
            else
                animator.SetBool("Died", true);

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
            OnHit?.Invoke(gameObject.GetComponent<ZombieAI>());
        }
    }

}
