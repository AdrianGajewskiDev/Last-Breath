using LB.GameMechanics;
using LB.InputControllers;
using LB.Player;
using LB.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LB.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Singleton;

        public PlayerInventoryManager PlayerInventory;

        [SerializeField] GameObject statsPanel;

        [SerializeField] GameObject levelFinPanel;
        [SerializeField] GameObject pauseMenuPanel;

        [SerializeField] Text AmmoDisplayer;

        [SerializeField] Text ScoreDisplayer;

        [SerializeField] Text ZombieKilled;

        [SerializeField] Text LevelCounter;

        [SerializeField] Text LevelFinText;

        public Text PauseMenuText;

        [SerializeField] RawImage bloodOverlay;

        [SerializeField] Image DeathScreen;

        public Text MessageDisplayer;

        bool showStatsMenu = false;
        bool showPauseMenu = false;
        bool showOptionsMenu = false;

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

        public IEnumerator PlayLevelFinishedAnimation()
        {
            levelFinPanel.GetComponent<Animation>().Play();
            Crosshair.Singleton.HideCrosshair = true;
            yield return new WaitForSeconds(3f);
            Crosshair.Singleton.HideCrosshair = false;
        }

        public void PauseMenuSlideIn()
        {
            showPauseMenu = true;
            Crosshair.Singleton.HideCrosshair = true;
            PlayerInventoryManager.Singleton.CurrentWeapon.GetComponent<Weapon>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenuPanel.GetComponent<Animation>().Play("PauseMenuSlideIn");
        }
        public void PauseMenuSlideOut()
        {
            Crosshair.Singleton.HideCrosshair = false;
            showPauseMenu = false;
            PlayerInventoryManager.Singleton.CurrentWeapon.GetComponent<Weapon>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuPanel.GetComponent<Animation>().Play("PauseMenuSlideOut");
        }

        public void OptionsMenuSlideIn()
        {
            pauseMenuPanel.GetComponent<Animation>().Play("OptionsMenuSlideIn");
        }

        public void OptionsMenuSlideOut()
        {
            pauseMenuPanel.GetComponent<Animation>().Play("OptionsMenuSlideOut");
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
            LevelCounter.text = $"Level: {LevelManager.Singleton.CurrentLevel}";
            LevelFinText.text = $"{LevelCounter.text}";
        }

        private void Update()
        {
            UpdateIU();

            if (InputController.ShowStats)
                SwitchStatsDisplayer(!showStatsMenu);

            if (InputController.ShowPauseMenu && showPauseMenu == false)
            {
                PauseMenuSlideIn();
            }
            else if (InputController.ShowPauseMenu && showPauseMenu == true)
            {
                PauseMenuSlideOut();
            }

        }
    }

}
