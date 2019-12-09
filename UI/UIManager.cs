using LB.GameMechanics;
using LB.InputControllers;
using LB.Player;
using LB.Player.Inventory;
using LB.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace LB.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Other Properties
        public static UIManager Singleton;

        public PlayerInventoryManager PlayerInventory; 
        #endregion

        #region Panels

        [SerializeField] GameObject statsPanel;
        [SerializeField] GameObject levelFinPanel;
        [SerializeField] GameObject pauseMenuPanel;
        [SerializeField] GameObject questEndPanel;

        #endregion

        #region Text Properties

        [SerializeField] Text AmmoDisplayer;

        [SerializeField] Text ScoreDisplayer;

        [SerializeField] Text ZombieKilled;

        [SerializeField] Text LevelCounter;

        [SerializeField] Text LevelFinText;
        [SerializeField] Text curFinQuestName;

        public Text MessageDisplayer;
        public Text PauseMenuText;
        #endregion

        #region Images
        [SerializeField] RawImage bloodOverlay;

        [SerializeField] Image DeathScreen;
        public Image BatteryStatus;

        #endregion

        #region booleans
        bool showStatsMenu = false;
        bool showPauseMenu = false;
        bool showBatteryStatus = false;

        #endregion

        #region UI Methods
        private void Awake()
        {
            Singleton = this;
            bloodOverlay.enabled = false;
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

            if (InputController.UseItem && showBatteryStatus == false)
            {
                BatteryStatus.enabled = true;
                showBatteryStatus = true;
            }
            else if (InputController.UseItem && showBatteryStatus == true)
            {
                BatteryStatus.enabled = false;
                showBatteryStatus = false;
            }

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
            LevelManager.Singleton.localPlayer.GetComponent<FirstPersonController>().enabled = false;
            showPauseMenu = true;
            Crosshair.Singleton.HideCrosshair = true;
            if (PlayerInventoryManager.Singleton.CurrentWeapon != null)
            {
                PlayerInventoryManager.Singleton.CurrentWeapon.GetComponent<Weapon>().enabled = false;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenuPanel.GetComponent<Animation>().Play("PauseMenuSlideIn");
        }
        public void PauseMenuSlideOut()
        {
            LevelManager.Singleton.localPlayer.GetComponent<FirstPersonController>().enabled = true;

            Crosshair.Singleton.HideCrosshair = false;
            showPauseMenu = false;
            if (PlayerInventoryManager.Singleton.CurrentWeapon != null)
            {
                PlayerInventoryManager.Singleton.CurrentWeapon.GetComponent<Weapon>().enabled = true;
            }
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
        public IEnumerator ShowQuestEndScreen(string questName)
        {
            Crosshair.Singleton.HideCrosshair = true;
            curFinQuestName.text = $"Congrats, you have just finished {questName} quest";
            questEndPanel.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(5f);
            Crosshair.Singleton.HideCrosshair = false;
        }
        private void SwitchStatsDisplayer(bool v)
        {
            statsPanel.SetActive(v);
            showStatsMenu = !showStatsMenu;
        }
        private void UpdateIU()
        {
            if (PlayerInventory.CurrentWeapon == null)
            {
                AmmoDisplayer.text = "-/-";
            }
            else
            {
                AmmoDisplayer.text = $"{PlayerInventory.CurrentWeapon.CurrentAmmoInClip} / {PlayerInventory.CurrentWeapon.MaxAmmo}";
            }

            ScoreDisplayer.text = $"Score: {PlayerStats.Singleton.Score}";
            ZombieKilled.text = $"Zombies Killed: {PlayerStats.Singleton.KilledZombies}";
            LevelCounter.text = $"Level: {LevelManager.Singleton.CurrentLevel}";
            LevelFinText.text = $"{LevelCounter.text}";
        }
        #endregion
    }

}
