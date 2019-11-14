using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;

    public PlayerInventoryManager PlayerInventory;

    [SerializeField] Text AmmoDisplayer;

    [SerializeField] RawImage bloodOverlay;

    [SerializeField] Image DeathScreen;

    public Text MessageDisplayer;

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

    public void DeathScreenFadeIn()
    {
        DeathScreen.GetComponent<Animation>().Play();
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
