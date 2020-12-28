using LB.Player.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace LB.GameMechanics
{
    public abstract class PickupAble : MonoBehaviour, IInventoryItem
    {
        public Sprite Image;

        public abstract void Execute();

        public void ExecuteAction()
        {
            Execute();
        }

        public abstract string GetName();
    }
}