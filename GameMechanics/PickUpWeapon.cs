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

        public string GetName() => $"Pick up a {WeaponPrefab.name}";

        public Type ItemType() => Type.Item;
    }

}
