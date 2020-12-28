using LB.Player;
using LB.Player.Inventory;
using LB.UI;
using UnityEngine;

namespace LB.GameMechanics
{
    public class PickUpWeapon : PickupAble
    {
        public GameObject WeaponPrefab;

        public override void Execute()
        {
            PlayerStats.Singleton.AddEXP(3f);
            PlayerInventoryManager.Singleton.AddWeapon(WeaponPrefab);
            UIManager.Singleton.MessageDisplayer.text = string.Empty;
            Destroy(gameObject, 1f);
        }

        public override string GetName() => $"Pick up a {WeaponPrefab.name}";

    }

}
