using LB.Health;
using LB.Player;
using UnityEngine;

namespace LB.GameMechanics
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

        public GameMode GameMode;
        public GameObject localPlayer;

        public bool UseAutoSave = false;

        bool saveGame = true;

        public void SavePlayerProgress()
        {
            SaveSystem.SavePlayerStats(localPlayer.GetComponent<PlayerHealth>().GetCurrentHealth(),
                                       localPlayer.GetComponent<PlayerStats>().GetCurrentPlayerLevel(),
                                       localPlayer.GetComponent<PlayerStats>().ExperiencePoint);
        }

        void LoadPlayerProgress()
        {
            var stats = SaveSystem.LoadPlayerStats();

            localPlayer.GetComponent<PlayerHealth>().SetCurrentHealth(stats.CurrentHealth);
            localPlayer.GetComponent<PlayerStats>().SetPlayerEXP(stats.CurrentPlayerExperience);
            localPlayer.GetComponent<PlayerStats>().SetPlayerCurrentLevel(stats.CurrentPlayerLevel);
        }

        private void Update()
        {
            if (saveGame == true && UseAutoSave == true)
            {
                saveGame = false;

                Timer.Singleton.Add(() =>
                {
                    Debug.Log("Save");
                    saveGame = true;


                }, 5f);
            }
        }

        private void Start()
        {
           
            Singleton = this;
            localPlayer = GameObject.FindGameObjectWithTag("Player");
            LoadPlayerProgress();
        }

    }
}
