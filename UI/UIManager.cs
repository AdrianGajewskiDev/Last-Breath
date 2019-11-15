using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;

    public PlayerInventoryManager PlayerInventory;

    [SerializeField] GameObject statsPanel;

    [SerializeField] Text AmmoDisplayer;

    [SerializeField] Text ScoreDisplayer;

    [SerializeField] Text ZombieKilled;

    [SerializeField] RawImage bloodOverlay;

    [SerializeField] Image DeathScreen;

    public Text MessageDisplayer;

    bool showStatsMenu = false;

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

    private void SwitchStatsDisplayer(bool v)
    {
        statsPanel.SetActive(v);
        showStatsMenu = !showStatsMenu;
    }


    private void UpdateIU()
    {
        AmmoDisplayer.text = $"{PlayerInventory.CurrentWeapon.CurrentAmmoInClip} / {PlayerInventory.CurrentWeapon.MaxAmmo}";
        ScoreDisplayer.text = $"Score: {PlayerStats.Singleton.Score}";
        ZombieKilled.text = $"Zomies Killed: {PlayerStats.Singleton.KilledZombies}";
    }

    private void Update()
    {
        UpdateIU();

        if (InputController.ShowStats)
            SwitchStatsDisplayer(!showStatsMenu);
    }
}
