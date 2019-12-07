using UnityEngine;

namespace LB.Player.Inventory
{
    public class QuestItems : IInventoryItem
    {
        public void ExecuteAction()
        {
            Debug.Log("Works");
        }
    }
}
