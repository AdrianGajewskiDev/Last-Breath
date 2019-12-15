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

        private void Start()
        {
            Singleton = this;
            localPlayer = GameObject.FindGameObjectWithTag("Player");
            LoadPlayerProgress();
        }
    }
}
