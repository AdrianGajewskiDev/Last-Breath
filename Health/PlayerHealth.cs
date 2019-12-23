using LB.GameMechanics;
using LB.Player;
using LB.Player.Inventory;
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

        public void SetCurrentHealth(int value) => currentHealth = value;
        public int GetMaxHealth() => maxHealth;
        public int GetCurrentHealth() => currentHealth;

        public void GiveDamage(int ammount)
        {
            currentHealth -= ammount;

            OnHit?.Invoke();
        }

        public void AddHealth(int h) => currentHealth += h;

        public bool IsDead() => currentHealth <= 0;

        void Start()
        {
            //currentHealth = maxHealth;
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
            if(PlayerInventoryManager.Singleton.CurrentWeapon != null) PlayerInventoryManager.Singleton.CurrentWeapon.enabled = false;
        }
    }

}
