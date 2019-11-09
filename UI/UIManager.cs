using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;

    public PlayerInventoryManager PlayerInventory;

    [SerializeField] Text AmmoDisplayer;

    [SerializeField] RawImage bloodOverlay;

    private void Awake()
    {
        Singleton = this;
        bloodOverlay.enabled = false;
    }

    public IEnumerator SetBloodOverlay()
    {
        bloodOverlay.enabled = true;
        yield return new WaitForSeconds(.3f);
        bloodOverlay.enabled = false;
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
