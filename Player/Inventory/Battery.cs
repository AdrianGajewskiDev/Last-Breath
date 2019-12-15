using LB.GameMechanics;
using LB.UI;
using UnityEngine;

namespace LB.Player.Inventory
{
    public class Battery : MonoBehaviour, IPickupAble
    {
        public void Execute()
        {
            GameManager.Singleton.localPlayer.GetComponentInChildren<Flashlight>().LoadBattery();
            UIManager.Singleton.MessageDisplayer.text = string.Empty;
            Destroy(gameObject);
        }

        public string GetName() => "Pick up a battery";

    }
}
