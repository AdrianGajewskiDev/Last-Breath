using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerInventoryManager PlayerInventory;

    public Text AmmoDisplayer;

    private void UpdateIU()
    {
        AmmoDisplayer.text = $"{PlayerInventory.CurrentWeapon.CurrentAmmoInClip} / {PlayerInventory.CurrentWeapon.MaxAmmo}";
    }

    private void Update()
    {
        UpdateIU();
    }
}
