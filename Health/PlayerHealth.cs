using LB.GameMechanics;
using LB.Player;
using LB.UI;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace LB.Health
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] int maxHealth;
        int currentHealth;

        public event System.Action OnHit;
        public void Die()
        {

            ZombiesManager.Singleton.gameOver = true;
            ShowDeathScreen();
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

        void ShowDeathScreen()
        {
            UIManager.Singleton.DeathScreenFadeIn();
            ZombiesManager.Singleton.DestroyAllZombies();
            this.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerInventoryManager.Singleton.CurrentWeapon.enabled = false;
        }
    }

}
