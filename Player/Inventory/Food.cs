using LB.GameMechanics;
using LB.Health;
using UnityEngine;

namespace LB.Player.Inventory
{
    public class Food :  PickUpAble
    {
        public int healthPoints;

        public override void Execute()
        {
            GameManager.Singleton.localPlayer.GetComponent<PlayerHealth>().AddHealth(healthPoints);
            PlayerStats.Singleton.AddEXP(2f);
            this.GetComponent<MeshRenderer>().enabled = false;
            PlayerInventoryManager.Singleton.AddItem(this);
        }

        public override string GetName() => $"Pick up a {this.gameObject.name}";
    }

}
