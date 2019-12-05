using LB.Player;
using LB.Player.Inventory;
using UnityEngine;

namespace LB.GameMechanics
{
    [RequireComponent(typeof(AudioSource))]
    public class AmmoBox : MonoBehaviour, IPickupAble
    {
        private AudioSource audioSource;
        public int AmmoAmount;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Execute()
        {
            if (PlayerInventoryManager.Singleton.CurrentWeapon != null)
            {
                PlayerInventoryManager.Singleton.CurrentWeapon.MaxAmmo += AmmoAmount;
                audioSource.Play();
                Destroy(this.gameObject, .3f);
            }
        }
    }

}

