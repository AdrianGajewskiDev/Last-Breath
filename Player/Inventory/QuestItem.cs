using LB.GameMechanics;
using UnityEngine;

namespace LB.Player.Inventory
{
    public class QuestItem : PickUpAble, IInventoryItem
    {

        public override void Execute()
        {
            PlayerInventoryManager.Singleton.questItems.Add(this);
        }
        
        public void ExecuteAction()
        {
            Debug.Log("Works");
        }

        public override string GetName() => $"Pick up a {this.gameObject.name}";

    }
}
