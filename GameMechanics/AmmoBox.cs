using LB.Player;
using LB.Player.Inventory;
using UnityEngine;

namespace LB.GameMechanics
{
    [RequireComponent(typeof(AudioSource))]
    public class AmmoBox : PickUpAble
    {
        private AudioSource audioSource;
        public int AmmoAmount;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public override void Execute()
        {
            if (PlayerInventoryManager.Singleton.CurrentWeapon != null)
            {
                PlayerInventoryManager.Singleton.CurrentWeapon.MaxAmmo += AmmoAmount;
                audioSource.Play();
                Destroy(this.gameObject, .3f);
            }
        }


        public override string GetName() => "Pick up a ammo";
    }

}

