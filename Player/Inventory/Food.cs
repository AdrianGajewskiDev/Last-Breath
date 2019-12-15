using LB.GameMechanics;
using LB.Health;
using UnityEngine;

namespace LB.Player.Inventory
{
    public class Food : MonoBehaviour, IPickupAble
    {
        public int healthPoints;

        public void Execute()
        {
            GameManager.Singleton.localPlayer.GetComponent<PlayerHealth>().AddHealth(healthPoints);
            PlayerStats.Singleton.AddEXP(2f);
            Destroy(this.gameObject);
        }

        public string GetName() => $"Pick up a {this.gameObject.name}";
    }

}
