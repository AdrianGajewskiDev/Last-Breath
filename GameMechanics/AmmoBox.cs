using LB.Player;
using UnityEngine;

namespace LB.GameMechanics
{
    [RequireComponent(typeof(AudioSource))]
    public class AmmoBox : MonoBehaviour
    {
        private AudioSource audioSource;
        public int AmmoAmount;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerInventoryManager.Singleton.CurrentWeapon.MaxAmmo += AmmoAmount;
            audioSource.Play();
            Destroy(gameObject, .3f);
        }
    }

}

