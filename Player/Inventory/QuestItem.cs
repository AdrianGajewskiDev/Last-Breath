using LB.GameMechanics;
using UnityEngine;

namespace LB.Player.Inventory
{
    public class QuestItem : MonoBehaviour, IInventoryItem, IPickupAble
    {

        public void Execute()
        {
            PlayerInventoryManager.Singleton.questItems.Add(this);
        }
        
        public void ExecuteAction()
        {
            Debug.Log("Works");
        }

        public string GetName() => $"Pick up a {this.gameObject.name}";

    }
}
