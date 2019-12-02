using LB.InputControllers;
using LB.Player;
using LB.Player.Inventory;
using LB.UI;
using UnityEngine;

namespace LB.GameMechanics
{
    public class PickUpWeapon : MonoBehaviour
    {
        public GameObject WeaponPrefab;

        private void OnTriggerEnter(Collider col)
        {

            UIManager.Singleton.MessageDisplayer.text = $"Pick up a {gameObject.name} [F]";

        }

        private void OnTriggerStay(Collider other)
        {
            if (InputController.PickUpItem)
            {
                PlayerInventoryManager.Singleton.AddWeapon(WeaponPrefab);
                UIManager.Singleton.MessageDisplayer.text = string.Empty;
                Destroy(gameObject, 1f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            UIManager.Singleton.MessageDisplayer.text = string.Empty;
        }
    }

}
