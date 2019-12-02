using LB.UI;
using UnityEngine;

namespace LB.Player.Inventory
{
    public class Battery : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            UIManager.Singleton.MessageDisplayer.text = "Pick up a battery [F]";    
        }

        private void OnTriggerStay(Collider other)
        {
            if(InputControllers.InputController.PickUpItem)
            {
                other.GetComponentInChildren<Flashlight>().LoadBattery();
                UIManager.Singleton.MessageDisplayer.text = string.Empty;
                Destroy(gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            UIManager.Singleton.MessageDisplayer.text = string.Empty;
        }
    }
}
