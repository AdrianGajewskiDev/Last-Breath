using LB.InputControllers;
using LB.Player;
using LB.Player.Inventory;
using LB.UI;
using UnityEngine;

namespace LB.GameMechanics
{
    public class PickUpWeapon : MonoBehaviour, IPickupAble
    {
        public GameObject WeaponPrefab;

        public void Execute()
        {

            PlayerInventoryManager.Singleton.AddWeapon(WeaponPrefab);
            UIManager.Singleton.MessageDisplayer.text = string.Empty;
            Destroy(gameObject, 1f);
        }
    }

}
