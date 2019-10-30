using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerInventoryManager PlayerInventory;

    public Text AmmoDisplayer;

    private void Awake()
    {
        PlayerInventory = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerInventoryManager>();
    }

    private void UpdateIU()
    {
        AmmoDisplayer.text = $"{PlayerInventory.CurrentWeapon.CurrentAmmoInClip} / {PlayerInventory.CurrentWeapon.MaxAmmo}";
    }

    private void Update()
    {
        UpdateIU();
    }
}
